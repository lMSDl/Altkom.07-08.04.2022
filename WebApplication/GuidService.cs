using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class GuidService
    {
        public string Key { get; set; }

        public GuidService()
        {
            Reset();
        }

        public void Reset()
        {
            Key = Guid.NewGuid().ToString();
        }
    }
}
