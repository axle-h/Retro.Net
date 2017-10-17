using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using GameBoy.Net.Graphics;
using GameBoy.Net.Peripherals;
using GameBoy.Net.Wiring;
using Shouldly;

namespace Retro.Net.Tests.GameBoy.Blargg
{
    public abstract class BlarggTestFixture
    {
        private static readonly TimeSpan Timeout = TimeSpan.FromMinutes(5);

        private const string BlarggArchiveResource = "Blargg.zip";

        public async Task RunAsync(string cartridgeResource)
        {
            var catridge = GetCartridgeBinary(cartridgeResource);
            var config = new BlarggTestGameBoyConfig(catridge);

            using (var context = new GameBoyContext(config, () => new NullRenderHandler()))
            using (var core = context.CoreContext.GetNewCore())
            {
                var io = core.GetPeripheralOfType<IGameBoyMemoryMappedIo>();

                var serialPort = new BlarggTestSerialPort(Timeout);
                io.HardwareRegisters.SerialPort.Connect(serialPort);

                using (var cancellation = new CancellationTokenSource())
                {
                    var token = cancellation.Token;
                    var coreProcess = Task.Run(() => core.StartCoreProcessAsync(token), token);
                    cancellation.CancelAfter(Timeout + TimeSpan.FromMinutes(1));
                    while (!cancellation.IsCancellationRequested)
                    {
                        var word = serialPort.WaitForNextWord();
                        if (word == null)
                        {
                            throw new Exception("Couldn't get next word");
                        }

                        if (word == "Failed" || word == "Passed")
                        {
                            await Task.Delay(TimeSpan.FromSeconds(1), token).ConfigureAwait(false);
                            cancellation.Cancel();

                            word.ShouldBe("Passed");
                            return;
                        }
                    }
                }
            }

        }

        private static byte[] GetCartridgeBinary(string cartridgeResource)
        {
            var type = typeof(BlarggTestFixture).GetTypeInfo();
            using (var stream = type.Assembly.GetManifestResourceStream($"{type.Namespace}.{BlarggArchiveResource}"))
            using (var zip = new ZipArchive(stream, ZipArchiveMode.Read))
            using (var entry = zip.GetEntry(cartridgeResource).Open())
            using (var ms = new MemoryStream())
            {
                entry.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
