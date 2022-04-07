using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public class StandardFontService : IFontService
    {
        public StandardFontService(IConfiguration configuration)
        {
            Console.WriteLine($"{configuration["Greetings:Greeting2"]} {configuration["Greetings:Targets:IA"]}!");
            Console.WriteLine("StandardFontService");
        }
        public string Render(string @string)
        {
            return Figgle.FiggleFonts.Standard.Render(@string);
        }
    }
}
