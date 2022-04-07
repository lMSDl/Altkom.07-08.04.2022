using BogusService;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public class PersonService
    {
        private IConsoleService _consoleService;
        private BogusService<Person> _bogusService;

        public PersonService(IConsoleService consoleService, BogusService<Person> bogusService)
        {
            _consoleService = consoleService;
            _bogusService = bogusService;
        }

        public void Show()
        {
            var people = _bogusService.Get();
            foreach (var item in people)
            {
                _consoleService.WriteLine($"{item.FirstName} {item.LastName}");
            }
        }
    }
}
