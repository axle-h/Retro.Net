using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GameBoy.Net;
using GameBoy.Net.Config;
using GameBoy.Net.Graphics;
using GameBoy.Net.Peripherals;
using GameBoy.Net.Wiring;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Retro.Net.Api.Middleware;
using Retro.Net.Api.RealTime;
using Retro.Net.Api.RealTime.Interfaces;
using Retro.Net.Wiring;

namespace Retro.Net.Api
{
    public class Startup
    {
        public const string ApiRootPath = "/api";
        public const string WebSocketRootPath = "/ws";

        private const string CartridgeResource = "cartridge.zip";
        
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterType<SingleCoreWebSocketContext>().As<IWebSocketContext>().SingleInstance();
            builder.RegisterType<WebSocketRenderer>().As<IRenderer>().As<IWebSocketRenderer>().InZ80Scope();
            builder.RegisterGeneric(typeof(FramedMessageHandler<>)).As(typeof(IFramedMessageHandler<>)).InZ80Scope();

            var cartridge = GetCartridgeBinary();
            var config = new StaticGameBoyConfig(cartridge, GameBoyType.GameBoy, true);
            builder.RegisterGameBoy(config);
            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime lifetime, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMiddleware<AngularRoutesMiddleware>();
            app.UseWebSockets();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }

        private static byte[] GetCartridgeBinary()
        {
            var type = typeof(Startup).GetTypeInfo();
            using (var stream = type.Assembly.GetManifestResourceStream($"{type.Namespace}.{CartridgeResource}"))
            using (var zip = new ZipArchive(stream, ZipArchiveMode.Read))
            using (var entry = zip.Entries.Single().Open())
            using (var ms = new MemoryStream())
            {
                entry.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
