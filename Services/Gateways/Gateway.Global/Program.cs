using EShopping.Utilities;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Gateway.Global
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* THIS SERILOG WILL WORK WITH ONLY "Microsoft.Extensions.Logging" */
            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                  .MinimumLevel.Warning()
                  .WriteTo.File("Logs/Service.Gateway.Log.txt", LogEventLevel.Warning,
                  flushToDiskInterval: TimeSpan.FromSeconds(1), rollingInterval: RollingInterval.Day)
                  //.WriteTo.Console()
                  .CreateLogger();

            /* Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                         .WriteTo.Console()
                         .CreateLogger();*/

            try
            {
                var kk = EnvironmentHelper.GetConfigurationPath(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
                Log.Warning("Starting gateway");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               // .UseSerilog()
                .ConfigureAppConfiguration((host, config) =>
                {                    
                    config.AddJsonFile(EnvironmentHelper.GetConfigurationPath(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
