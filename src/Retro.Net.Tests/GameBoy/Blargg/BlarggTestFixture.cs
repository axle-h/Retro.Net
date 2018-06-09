using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.OwnedInstances;
using GameBoy.Net;
using GameBoy.Net.Config;
using GameBoy.Net.Devices.Graphics;
using GameBoy.Net.Devices.Interfaces;
using GameBoy.Net.Peripherals;
using GameBoy.Net.Wiring;
using Retro.Net.Z80.Core.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Retro.Net.Tests.Util;
using Xunit.Abstractions;

namespace Retro.Net.Tests.GameBoy.Blargg
{
    public abstract class BlarggTestFixture
    {
        private static readonly TimeSpan Timeout = TimeSpan.FromMinutes(5);

        private const string BlarggArchiveResource = "Blargg.zip";

        private readonly ILoggerFactory _loggerFactory;

        protected BlarggTestFixture(ITestOutputHelper output)
        {
            _loggerFactory = new LoggerFactory().AddXunit(output);
        }

        public async Task RunAsync(string cartridgeResource)
        {
            var cartridge = GetCartridgeBinary(cartridgeResource);
            var config = new StaticGameBoyConfig(cartridge, GameBoyType.GameBoy, false);

            var builder = new ContainerBuilder();
            builder.RegisterType<NullRenderer>().As<IRenderer>().SingleInstance();

            var environment = Mock.Of<IHostingEnvironment>();
            Mock.Get(environment).SetupGet(x => x.EnvironmentName).Returns(EnvironmentName.Production);
            builder.RegisterGameBoy(environment, config);
            builder.RegisterInstance(_loggerFactory).As<ILoggerFactory>();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            using (var ownedCore = scope.Resolve<Owned<ICpuCore>>())
            {
                var core = ownedCore.Value;
                var io = core.GetPeripheralOfType<IGameBoyMemoryMappedIo>();

                var serialPort = new BlarggTestSerialPort();
                io.HardwareRegisters.SerialPort.Connect(serialPort);

                using (var cancellation = new CancellationTokenSource())
                {
                    var token = cancellation.Token;
                    Exception exception = null;
                    var coreProcess = Task.Run(async () =>
                                               {
                                                   try
                                                   {
                                                       await core.StartCoreProcessAsync(token);
                                                   }
                                                   catch (Exception e)
                                                   {
                                                       exception = e;
                                                       cancellation.Cancel();
                                                   }
                                               }, token);
                    cancellation.CancelAfter(Timeout + TimeSpan.FromMinutes(1));

                    try
                    {
                        while (!cancellation.IsCancellationRequested)
                        {
                            var word = serialPort.WaitForNextWord(cancellation.Token);
                            if (word == null)
                            {
                                throw new Exception("Couldn't get next word");
                            }

                            if (word == "Failed" || word == "Passed")
                            {
                                await Task.Delay(TimeSpan.FromSeconds(1), token);
                                cancellation.Cancel();

                                word.Should().Be("Passed");
                                return;
                            }
                        }
                    }
                    catch (OperationCanceledException e)
                    {
                        throw new Exception("Blargg test failed", exception ?? e);
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
