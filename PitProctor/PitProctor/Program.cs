using System;
using System.IO;

using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace PitProctor
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureServices();
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("The application is ready, up and running...");
            Console.ReadKey();
        }
        static void ConfigureLogging()
        {
            var config = new LoggingConfiguration();

            var logfile = new FileTarget("logfile")
            {
                FileName = "log/" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + DateTime.Today.Year + "/Server.log",
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };
            var logconsole = new ConsoleTarget("logconsole")
            {
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            NLog.LogManager.Configuration = config;
        }
        
        static void ConfigureServices()
        {
            ConfigureLogging();
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Configured the Logging");
            var serviceProvider = new ServiceCollection()
                .BuildServiceProvider();
            logger.Info("Configured the Services and Dependency Injection");
        }
    }
}
