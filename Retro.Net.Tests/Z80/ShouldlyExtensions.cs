using System;
using System.Collections.Generic;
using System.Linq;
using Retro.Net.Timing;
using Retro.Net.Z80.Core.Decode;
using Shouldly;

namespace Retro.Net.Tests.Z80
{
    internal static class ShouldlyExtensions
    {
        public static void ShouldBe(this InstructionTimings observed, InstructionTimings expected) =>
            observed.ShouldSatisfyAllConditions($"[exptected: {expected}, observed: {observed}", Assertions(observed, expected).ToArray());

        public static void ShouldBe(this Operation observed, Operation expected) =>
            observed.ShouldSatisfyAllConditions($"[exptected: {expected}, observed: {observed}", Assertions(observed, expected).ToArray());

        private static IEnumerable<Action> Assertions(InstructionTimings observed, InstructionTimings expected)
        {
            yield return () => observed.MachineCycles.ShouldBe(expected.MachineCycles, nameof(observed.MachineCycles));
            yield return () => observed.ThrottlingStates.ShouldBe(expected.ThrottlingStates, nameof(observed.ThrottlingStates));
        }

        private static IEnumerable<Action> Assertions(Operation expected, Operation observed)
        {
            yield return () => observed.Address.ShouldBe(expected.Address);
            yield return () => observed.ByteLiteral.ShouldBe(expected.ByteLiteral);
            yield return () => observed.Displacement.ShouldBe(expected.Displacement);
            yield return () => observed.FlagTest.ShouldBe(expected.FlagTest);
            yield return () => observed.OpCode.ShouldBe(expected.OpCode);
            yield return () => observed.OpCodeMeta.ShouldBe(expected.OpCodeMeta);
            yield return () => observed.Operand1.ShouldBe(expected.Operand1);
            yield return () => observed.Operand2.ShouldBe(expected.Operand2);
            yield return () => observed.WordLiteral.ShouldBe(expected.WordLiteral);
        }
    }
}