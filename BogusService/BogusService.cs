using BogusService.Fakers;
using System;
using System.Collections.Generic;

namespace BogusService
{
    public class BogusService<T> where T : class
    {
        private int _count;
        private BaseFaker<T> _faker;

        public BogusService(int count, BaseFaker<T> faker)
        {
            _count = count;
            _faker = faker;
        }

        public IEnumerable<T> Get()
        {
            return _faker.Generate(_count);
        }

    }
}
