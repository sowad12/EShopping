
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;

namespace Discount.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //     .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //     .MinimumLevel.Warning()
            //     .WriteTo.File("Logs/Services/Catalog/Catalog.Api.Log.txt", LogEventLevel.Warning,
            //     flushToDiskInterval: TimeSpan.FromSeconds(1), rollingInterval: RollingInterval.Day)
            //     .WriteTo.Console()
            //     .CreateLogger();


            try
            {
                Log.Warning("Starting  host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Error details: {ex.Message}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
              //.UseSerilog()
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
                   webBuilder.ConfigureKestrel(options =>
                   {
                       // Setup an HTTP/2 endpoint without TLS
                       options.ListenAnyIP(5268, o => o.Protocols = HttpProtocols.Http2);
                   });
               })
               .UseDefaultServiceProvider(options => options.ValidateScopes = false);
    }
}
