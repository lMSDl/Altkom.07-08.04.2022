using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public class ConsoleService : IConsoleService
    {
        private IFontService _fontService;
        private ILogger<ConsoleService> _logger;

        public ConsoleService(IFontService fontService, ILogger<ConsoleService> logger)
        {
            Console.WriteLine("ConsoleService");
            _fontService = fontService;
            _logger = logger;
        }

        private int _counter;

        public void WriteLine(string @string)
        {
            Console.WriteLine(_fontService.Render(@string));
            _logger.LogInformation((++_counter).ToString());
        }
    }
}
