using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Middleware
{
    public class Use3Middleware : IMiddleware
    {
        private GuidService _guid;

        public Use3Middleware(GuidService guid)
        {
            _guid = guid;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Before next in use3");

            _guid.Reset();
            await next(context);

            Console.WriteLine(context.Response.StatusCode);
            Console.WriteLine("After next in use3");
        }
    }
}
