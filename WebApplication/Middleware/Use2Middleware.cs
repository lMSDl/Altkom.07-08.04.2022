using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Middleware
{
    public class Use2Middleware
    {

        private RequestDelegate _next;
        private GuidService _guid;

        public Use2Middleware(RequestDelegate next, GuidService guidService)
        {
            _next = next;
            _guid = guidService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Before next in use2");
            Console.WriteLine(_guid.Key);
            await _next(context);
            Console.WriteLine(_guid.Key);

            Console.WriteLine("After next in use2");
        }
    }
}
