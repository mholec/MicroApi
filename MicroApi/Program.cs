using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MicroApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host
                .CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureLogging(builder =>
                    {
                        builder.AddApplicationInsights();
                    });
                })
                .Build();

            try
            {
                host.Run();
            }
            catch (Exception e)
            {
                IConfigurationRoot config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
                TelemetryConfiguration.Active.InstrumentationKey = config["APPINSIGHTS_KEY"];
                new TelemetryClient(TelemetryConfiguration.Active).TrackException(e);
                
                throw;
            }
        }
    }
}