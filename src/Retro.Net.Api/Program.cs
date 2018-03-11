using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Retro.Net.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                       .UseSerilog((ctx, c) => c.ReadFrom.Configuration(ctx.Configuration))
                       .UseStartup<Startup>()
                       .Build();

            await host.RunAsync();
        }
    }
}
