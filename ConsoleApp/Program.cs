using ConsoleApp.Configurations;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            MakeConfiguration();


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
