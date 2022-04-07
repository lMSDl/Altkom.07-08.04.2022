using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BogusService.Fakers
{
    public class PersonFaker : BaseFaker<Person>
    {
        public PersonFaker()
        {
            RuleFor(x => x.FirstName, x => x.Person.FirstName);
            RuleFor(x => x.LastName, x => x.Person.LastName);
        }
    }
}
