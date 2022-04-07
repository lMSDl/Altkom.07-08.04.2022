using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public class SubZeroFontService : IFontService
    {
        public SubZeroFontService()
        {
            Console.WriteLine("SubZeroFontService");
        }

        public string Render(string @string)
        {
            return Figgle.FiggleFonts.SubZero.Render(@string);
        }
    }
}
