using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Identity.Main.Extensions.Configurations
{
    public static class ConfigurationExtensions
    {
        public static X509Certificate2 GetCertificate(this IConfiguration _config)
        {
            var certPath = _config.GetSection("certificate").GetSection("certPath").Value;
            var pass = _config.GetSection("certificate").GetSection("passPhrase").Value;
            var certificate = new X509Certificate2(certPath, pass, X509KeyStorageFlags.MachineKeySet);
            return certificate;
        }

    }
}
