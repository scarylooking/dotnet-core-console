using System;
using System.IO;
using example.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.Linq;

namespace example
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        private static void Main(string[] args)
        {
            ConfigureLogger(args);

            try
            {
                BuildServiceCollection();
                Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Encountered an unhandled exception");
            }
            finally
            {
                Log.Verbose("application has terminated gracefully");
                Log.CloseAndFlush();
            }
        }

        private static void Run()
        {
            using (_serviceProvider as IDisposable)
            {
                var application = _serviceProvider.GetRequiredService<Application>();
                application.Run();

                Console.WriteLine("Press ANY key to exit");
                Console.ReadKey();
            }
        }

      private static void ConfigureLogger(string[] args)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"logs\.log");
            var logEventLevel = GetConsoleLogLevel(args);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(logEventLevel)
                .WriteTo.File(path, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        private static void BuildServiceCollection()
        {
            try
            {
                Log.Information("Building configuration...");

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, false)
                    .Build();

                Log.Information("Building service provider...");

                _serviceProvider = new ServiceCollection()
                    .AddTransient<Application>()
                    .AddSingleton<IConfiguration>(configuration)
                    .AddSingleton<IUtility, Utility>()
                    .AddLogging(builder =>
                    {
                        builder.ClearProviders();
                        builder.AddSerilog();
                    })
                    .BuildServiceProvider();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Exception encountered while building configuration and service provider");
                throw;
            }
        }

        private static LogEventLevel GetConsoleLogLevel(string[] args)
        {
            var verbose = args.Contains("--verbose", StringComparer.CurrentCultureIgnoreCase) ||
                          args.Contains("-v", StringComparer.CurrentCultureIgnoreCase);

            return verbose ? LogEventLevel.Information : LogEventLevel.Fatal;
        }
    }
}