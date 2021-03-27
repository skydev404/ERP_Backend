using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd_ERP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string aspnetcore_environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine($"env: {aspnetcore_environment}");
            string configFilePath = string.Format("appsettings.{0}.json", aspnetcore_environment);

            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile(configFilePath, optional: true, reloadOnChange: true)
                .Build();

            LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
            var logger = NLogBuilder.ConfigureNLog(LogManager.Configuration).GetCurrentClassLogger();

            try
            {
                logger.Debug("HostBuilder Initializing");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .UseNLog();
    }
}
