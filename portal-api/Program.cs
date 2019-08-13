using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AmfValor.AmfMoney.PortalApi
{
    public class Program
    {
        private static string _environment;
        public static void Main(string[] args)
        {
            var webHostBuilder = new WebHostBuilder();
            _environment = webHostBuilder.GetSetting("environment");
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile(
                        "appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile(
                        $"appsettings.{_environment}.json", optional: true, reloadOnChange: true);
                })
                .UseSetting("https_port", "443")
                .UseStartup<Startup>();
    }
}
