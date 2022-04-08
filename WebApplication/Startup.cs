using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebApplication.Middleware;

namespace WebApplication
{
    public class Startup
    {
        public string Key { get; set; } = Guid.NewGuid().ToString();

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Use3Middleware>();
            services.AddSingleton<GuidService>();

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

            app.Map("/Bye", x => ConfigureBye(x, env));
            //app.UseMiddleware<Use2Middleware>();
            app.UseShowGuid();
            
            //app.UseMiddleware<Use3Middleware>();
            app.UseResetGuid();

            app.MapWhen(context => context.Request.Query.TryGetValue("name", out _), nameApp =>
            {
                nameApp.Run(async context =>
                {
                    Console.WriteLine("Before nameApp response");
                    await context.Response.WriteAsync($"Hello {context.Request.Query["name"]}!");
                    Console.WriteLine("After nameApp response");
                });
            });

            app.UseMiddleware<UseTerminalMiddleware>();
            //app.Run(async context =>
            //{
            //    Console.WriteLine("Before response");
            //    await context.Response.WriteAsync("Hello!");
            //    Console.WriteLine("After response");
            //});
        }

        private static void ConfigureBye(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
                {
                    Console.WriteLine("Before Bye next in use1");

                    await next();

                    Console.WriteLine("After Bye next in use1");
                });
            if (env.IsProduction())
            {

                app.Map("/2", bye2App =>
                    {
                        bye2App.Run(async context =>
                        {
                            Console.WriteLine("Before Bye response");
                            await context.Response.WriteAsync("bye 2");
                            Console.WriteLine("After Bye response");
                        });

                    });
            }

            app.Run(async context =>
                {
                    Console.WriteLine("Before Bye response");
                    await context.Response.WriteAsync("Bye!");
                    Console.WriteLine("After Bye response");
                });
        }
    }
}
