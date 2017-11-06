using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Retro.Net.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureLogging((h, builder) =>
                                  {
                                      h.HostingEnvironment.ConfigureNLog("nlog.xml");
                                      builder.SetMinimumLevel(LogLevel.Trace);
                                  })
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .UseSetting(WebHostDefaults.PreventHostingStartupKey, bool.TrueString)
                .UseUrls("http://localhost:2500/")
                .Build();

            host.Run();
        }
    }
}
