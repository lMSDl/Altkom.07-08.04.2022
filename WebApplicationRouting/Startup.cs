using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationRouting
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                await next();
            });

            app.Map("/inside", insideApp =>
            {
                insideApp.UseRouting();

                insideApp.Use(async (context, next) =>
                {
                    await next();
                });

                insideApp.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/Hello", async context =>
                    {
                        await context.Response.WriteAsync("Hello from inside World!");
                    });
                });
            });

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Hello", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGet("/Bye", async context =>
                {
                    await context.Response.WriteAsync("Bye World!");
                });
                endpoints.MapGet("/inside/Hello", async context =>
                {
                    await context.Response.WriteAsync("Hello from inside Main World!");
                });
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Under costruction...");
            });
        }
    }
}
