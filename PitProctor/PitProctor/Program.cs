using System;
using System.IO;

using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Config;
using NLog.Targets;
using PitProctor.Interfaces;
using PitProctor.Services;

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
            var config = new NLog.Config.LoggingConfiguration();
            //Console.WriteLine("log/" + DateTime.Today.Month+"-"+DateTime.Today.Day+ "-"+DateTime.Today.Year + "/Server.log");
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log/" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + DateTime.Today.Year + "/Server.log" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

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
               .AddSingleton<IFooService, FooService>()
               .AddSingleton<IBarService, BarService>()
               .BuildServiceProvider();
            logger.Info("Configured the Services and Dependency Injection");
            //var bar = serviceProvider.GetService<IBarService>();
            //bar.DoSomeRealWork();
        }
    }
}
