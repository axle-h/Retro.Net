using System.Threading.Tasks;
using Xunit;

namespace Retro.Net.Tests.GameBoy.Blargg
{
    public class CpuInstructionsTests : BlarggTestFixture
    {
        [Fact] public async Task cpu_instrs_01_special() => await RunAsync("01-special.gb");

        [Fact(Skip = "the block system delays interrupts too long for this test")]
        public async Task cpu_instrs_02_interrupts() => await RunAsync("02-interrupts.gb");

        [Fact] public async Task cpu_instrs_03_op_sp_hl() => await RunAsync("03-op sp,hl.gb");

        [Fact] public async Task cpu_instrs_04_op_r_imm() => await RunAsync("04-op r,imm.gb");

        [Fact] public async Task cpu_instrs_05_op_rp() => await RunAsync("05-op rp.gb");

        [Fact] public async Task cpu_instrs_06_ld_r_r() => await RunAsync("06-ld r,r.gb");

        [Fact] public async Task cpu_instrs_07_jr_jp_call_ret_rst() => await RunAsync("07-jr,jp,call,ret,rst.gb");

        [Fact] public async Task cpu_instrs_08_misc_instrs() => await RunAsync("08-misc instrs.gb");

        [Fact] public async Task cpu_instrs_09_op_r_r() => await RunAsync("09-op r,r.gb");

        [Fact] public async Task cpu_instrs_10_bit_ops() => await RunAsync("10-bit ops.gb");

        [Fact] public async Task cpu_instrs_11_op_a_mhl() => await RunAsync("11-op a,(hl).gb");
    }
}
