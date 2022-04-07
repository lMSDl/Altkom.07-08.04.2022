using ConsoleApp.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Models;
using System;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static IConfiguration Configuration { get; set; }
        static IServiceProvider ServiceProvider { get; set; }

        static void Main(string[] args)
        {
            MakeConfiguration();
            MakeServiceProvider();
            LoggingDemo();
        }

        private static void LoggingDemo()
        {
            var logger = ServiceProvider.GetService<ILogger<Program>>();

            logger.LogTrace("Trace");
            logger.LogDebug("Debug");
            logger.LogInformation("Information");
            logger.LogWarning("Warrning");
            logger.LogError("Error");
            logger.LogCritical("Critical");

            using (var scope = logger.BeginScope("For counter"))
            {
                for (int i = 0; i < 10; i++)
                {
                    logger.LogInformation(i.ToString());
                }
            }
        }

        private static void MakeServiceProvider()
        {
            //package Microsoft.Extensions.DependencyInjection
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(options => options
                .AddConfiguration(Configuration.GetSection("Logging"))
                //.SetMinimumLevel(LogLevel.Trace)
                .AddConsole(/*x => x.IncludeScopes = true*/)
                .AddDebug()
                .AddEventLog());
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigurationDemo()
        {
            var section = Configuration.GetSection("Greetings");
            var subsection = section.GetSection("Targets");

            Console.WriteLine($"{section["Greeting1"]} {subsection["Person"]}!");
            Console.WriteLine($"{Configuration["Greetings:Greeting2"]} {Configuration["Greetings:Targets:IA"]}!");

            var greetings = new Greetings();
            Configuration.GetSection("Greetings").Bind(greetings);

            var appConfig = new AppConfig();
            //package Microsoft.Extensions.Configuration.Binder
            Configuration.Bind(appConfig);

            //var no = int.Parse(config["NumberOfRepeats"]);
            var no = Configuration.GetValue<int>("NumberOfRepeats");
            for (int i = 0; i < no; i++)
            {
                Console.WriteLine($"{greetings.Greeting1} {greetings.Targets.Person}!");
                Console.WriteLine($"{appConfig.Greetings.Greeting2} {appConfig.Greetings.Targets.IA}!");
            }
        }

        private static void MakeConfiguration()
        {
            //package Microsoft.Extensions.Configuration
            Configuration = new ConfigurationBuilder()
                //package Microsoft.Extensions.Configuration.FileExtensions
                //package Microsoft.Extensions.Configuration.Json
                .AddJsonFile("Configurations/config.json", optional: true)
                //package Microsoft.Extensions.Configuration.Xml
                .AddXmlFile("Configurations/config.xml", optional: false, reloadOnChange: true)
                //package Microsoft.Extensions.Configuration.ini
                .AddIniFile("Configurations/config.ini")
                //package NetEscapades.Configuration.Yaml
                .AddYamlFile("Configurations/config.yaml")
                .Build();
        }
    }
}
