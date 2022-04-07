using ConsoleApp.Configurations;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //package Microsoft.Extensions.Configuration
            var config = new ConfigurationBuilder()
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

            var section = config.GetSection("Greetings");
            var subsection = section.GetSection("Targets");
            
            Console.WriteLine($"{section["Greeting1"]} {subsection["Person"]}!");
            Console.WriteLine($"{config["Greetings:Greeting2"]} {config["Greetings:Targets:IA"]}!");

            var greetings = new Greetings();
            config.GetSection("Greetings").Bind(greetings);

            var appConfig = new AppConfig();
            //package Microsoft.Extensions.Configuration.Binder
            config.Bind(appConfig);

            //var no = int.Parse(config["NumberOfRepeats"]);
            var no = config.GetValue<int>("NumberOfRepeats");
            for (int i = 0; i < no; i++)
            {
                Console.WriteLine($"{greetings.Greeting1} {greetings.Targets.Person}!");
                Console.WriteLine($"{appConfig.Greetings.Greeting2} {appConfig.Greetings.Targets.IA}!");
            }


        }
    }
}
