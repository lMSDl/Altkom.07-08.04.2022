using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Middleware
{
    public class UseTerminalMiddleware
    {
        public UseTerminalMiddleware(RequestDelegate _)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Before response");
            await context.Response.WriteAsync("Hello!");
            Console.WriteLine("After response");
        }
    }
}
