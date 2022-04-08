using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace WebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                Console.WriteLine("Before next in use1");

                await next();

                Console.WriteLine("After next in use1");
            });

            app.Map("/Bye", byeApp =>
            {
                byeApp.Use(async (context, next) =>
                 {
                     Console.WriteLine("Before Bye next in use1");

                     await next();

                     Console.WriteLine("After Bye next in use1");
                 });

                byeApp.Map("/2", bye2App =>
                {
                    bye2App.Run(async context =>
                    {
                        Console.WriteLine("Before Bye response");
                        await context.Response.WriteAsync("bye 2");
                        Console.WriteLine("After Bye response");
                    });

                });

                byeApp.Run(async context =>
                {
                    Console.WriteLine("Before Bye response");
                    await context.Response.WriteAsync("Bye!");
                    Console.WriteLine("After Bye response");
                });
            });


            app.Use(async (context, next) =>
            {
                Console.WriteLine("Before next in use2");

                await next();

                Console.WriteLine("After next in use2");
            });
            app.Use(async (context, next) =>
            {
                Console.WriteLine("Before next in use3");

                await next();

                Console.WriteLine("After next in use3");
            });

            app.MapWhen(context => context.Request.Query.TryGetValue("name", out _), nameApp =>
            {
                nameApp.Run(async context =>
                {
                    Console.WriteLine("Before nameApp response");
                    await context.Response.WriteAsync($"Hello {context.Request.Query["name"]}!");
                    Console.WriteLine("After nameApp response");
                });
            });

            app.Run(async context =>
            {
                Console.WriteLine("Before response");
                await context.Response.WriteAsync("Hello!");
                Console.WriteLine("After response");
            });
        }
    }
}
