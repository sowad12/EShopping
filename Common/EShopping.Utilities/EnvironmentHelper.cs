//using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EShopping.Utilities
{
    public class EnvironmentHelper
    {
        public static IConfigurationBuilder GetAppSettings(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            return builder;
        }

        public static string GetConfigurationPath(string environment)
        {
            switch (environment)
            {
                case "prod":
                    return Path.Combine("configuration", "configuration.prod.json");
                case "staging":
                    return Path.Combine("configuration", "configuration.staging.json");
                default:
                    return Path.Combine("configuration", "configuration.json");
            }
        }
    }
}
