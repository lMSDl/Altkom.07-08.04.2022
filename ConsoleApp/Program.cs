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

            while (true)
            {
                Console.WriteLine($"Hello {config["HelloJson"]}!");
                Console.WriteLine($"Hello {config["HelloXml"]}!");
                Console.WriteLine($"Hello {config["HelloIni"]}!");
                Console.WriteLine($"Hello {config["HelloYaml"]}!");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
}
