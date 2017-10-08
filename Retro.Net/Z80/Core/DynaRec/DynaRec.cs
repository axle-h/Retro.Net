using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AgileObjects.ReadableExpressions;
using Retro.Net.Memory;
using Retro.Net.Timing;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Peripherals;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Timing;
using Retro.Net.Z80.Util;

namespace Retro.Net.Z80.Core.DynaRec
{
    /// <summary>
    /// Instruction block factory using a dynamic translation from Z80 operations to expression trees.
    /// </summary>
    /// <seealso cref="IInstructionBlockFactory" />
    public partial class DynaRec : IInstructionBlockFactory
    {
        private readonly CpuMode _cpuMode;
        private readonly bool _debug;

        private bool _usesAccumulatorAndResult;
        private bool _usesDynamicTimings;
        private bool _usesLocalWord;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynaRec"/> class.
        /// </summary>
        /// <param name="platformConfig">The platform configuration.</param>
        /// <param name="runtimeConfig">The runtime configuration.</param>
        public DynaRec(IPlatformConfig platformConfig, IRuntimeConfig runtimeConfig) : this()
        {
            _cpuMode = platformConfig.CpuMode;
            _debug = runtimeConfig.DebugMode;
        }

        /// <summary>
        /// Gets an expression that synchronizes the program counter to the bytes read from teh prefetch queue.
        /// </summary>
        /// <value>
        /// An expression that synchronizes the program counter to the bytes read from teh prefetch queue.
        /// </value>
        private Expression SyncProgramCounter(DecodedBlock block) =>
            Expression.Assign(PC, Expression.Convert(Expression.Add(Expression.Convert(PC, typeof(int)), Expression.Constant(block.Length)), typeof(ushort)));

        /// <summary>
        /// Gets a value indicating whether this <see cref="IInstructionBlockFactory"/> [supports instruction block caching].
        /// </summary>
        /// <value>
        /// <c>true</c> if this <see cref="IInstructionBlockFactory"/> [supports instruction block caching]; otherwise, <c>false</c>.
        /// </value>
        public bool SupportsInstructionBlockCaching => true;

        /// <summary>
        /// Builds a new <see cref="IInstructionBlock"/> from the specified decoded block.
        /// </summary>
        /// <param name="block">The decoded instruction block.</param>
        /// <returns></returns>
        public IInstructionBlock Build(DecodedBlock block)
        {
            var lambda = BuildExpressionTree(block);
            var debugInfo = _debug
                ? $"{string.Join("\n", block.Operations.Select(x => x.ToString()))}\n\n{lambda.ToReadableString()}"
                : null;

            return new InstructionBlock(block.Address, block.Length, lambda.Compile(), block.Timings, block.Halt, block.Stop, debugInfo);
        }

        /// <summary>
        /// Builds the expression tree.
        /// </summary>
        /// <param name="block">The decoded block.</param>
        /// <returns></returns>
        private Expression<Func<IRegisters, IMmu, IAlu, IPeripheralManager, InstructionTimings>> BuildExpressionTree(DecodedBlock block)
        {
            // Reset
            _usesDynamicTimings = _usesLocalWord = _usesAccumulatorAndResult = false;

            // Run this first so we know what init & final expressions to add.
            var blockExpressions = block.Operations.SelectMany(o => Recompile(o, block)).ToArray();
            var initExpressions = GetBlockInitExpressions();
            var finalExpressions = GetBlockFinalExpressions(block);

            var expressions = initExpressions.Concat(blockExpressions).Concat(finalExpressions).ToArray();

            var expressionBlock = Expression.Block(GetParameterExpressions(), expressions);

            var lambda = Expression.Lambda<Func<IRegisters, IMmu, IAlu, IPeripheralManager, InstructionTimings>>(expressionBlock,
                Registers,
                Mmu,
                Alu,
                IO);
            return lambda;
        }

        /// <summary>
        /// Gets the parameter expressions of the expression block.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ParameterExpression> GetParameterExpressions()
        {
            if (_usesLocalWord)
            {
                yield return LocalWord;
            }

            if (_usesDynamicTimings)
            {
                yield return DynamicTimer;
            }

            if (_usesAccumulatorAndResult)
            {
                // Z80 supports some opcodes that manipulate the accumulator and a result in memory at the same time.
                yield return AccumulatorAndResult;
            }
        }

        /// <summary>
        /// Gets the expression block initialize expressions.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Expression> GetBlockInitExpressions()
        {
            if (_usesDynamicTimings)
            {
                // Create a new dynamic timer to record any timings calculated at runtime.
                yield return Expression.Assign(DynamicTimer, Expression.New(typeof (InstructionTimingsBuilder)));
            }
        }

        /// <summary>
        /// Gets any expressions required to finalize the expression block.
        /// </summary>
        /// <param name="block">The decoded block.</param>
        /// <returns></returns>
        private IEnumerable<Expression> GetBlockFinalExpressions(DecodedBlock block)
        {
            if (_debug)
            {
                yield return GetDebugExpression("Block Finalize");
            }

            if (_cpuMode == CpuMode.Z80)
            {
                // Add the block length to the 7 lsb of memory refresh register.
                var blockLengthExpression = Expression.Constant(block.Length, typeof (int));

                // Update Z80 specific memory refresh register
                yield return GetMemoryRefreshDeltaExpression(blockLengthExpression);
            }
            
            if (_usesDynamicTimings)
            {
                // Return the dynamic timings.
                yield return GetDynamicTimings;
            }
            else
            {
                // Return default timings.
                yield return Expression.Constant(default(InstructionTimings));
            }
        }
    }
}