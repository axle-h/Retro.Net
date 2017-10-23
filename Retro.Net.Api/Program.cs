using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Retro.Net.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .UseSetting(WebHostDefaults.PreventHostingStartupKey, bool.TrueString)
                .UseUrls("http://localhost:2500/")
                .Build();

            host.Run();
        }
    }
}
