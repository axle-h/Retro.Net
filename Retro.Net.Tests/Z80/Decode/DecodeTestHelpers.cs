using System;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;

namespace Retro.Net.Tests.Z80.Decode
{
    internal static class DecodeTestHelpers
    {
        public static PrimaryOpCode GetZ80IndexPrefix(this Operand index)
        {
            switch (index)
            {
                case Operand.IX:
                case Operand.IXh:
                case Operand.IXl:
                case Operand.mIXd:
                    return PrimaryOpCode.Prefix_DD;

                case Operand.IY:
                case Operand.IYh:
                case Operand.IYl:
                case Operand.mIYd:
                    return PrimaryOpCode.Prefix_FD;

                default:
                    throw new ArgumentException();
            }
        }
    }
}