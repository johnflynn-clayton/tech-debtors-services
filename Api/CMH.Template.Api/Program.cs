using CMH.VMF.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CMH.MobileHomeTracker.Api
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseClaytonLogging(config =>
                {
                    config.MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Trace;
                });
    }
}
