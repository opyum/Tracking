using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Diagnostics;
using System.Linq;


namespace Connexity.BC.Tracking.Service
{
    using Connexity.BC.Tracking.Service.Adapters.Spi.Configuration;

    public static class Program
    {
        public static void Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            Activity.ForceDefaultIdFormat = true;

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration(
                    (hostingContext, config) =>
                        {
                            var env = hostingContext.HostingEnvironment;
                            config.SetBasePath(env.ContentRootPath);
                            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                            config.AddJsonFile(
                                $"appsettings.{env.EnvironmentName}.json",
                                optional: true,
                                reloadOnChange: true);
                            config.AddEnvironmentVariables();

                            // Rebuild configuration
                            var configuration = config.Build();
                            var serviceConfigOptions =
                                configuration.GetSection("ServiceConfig").Get<ServiceConfigOptions>();
                        }).ConfigureWebHostDefaults(
                    webBuilder =>
                        {
                            webBuilder.UseStartup<Startup>();
                        }).UseSerilog(
                    (hostingContext, loggerConfiguration) =>
                        loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));
    }
}