using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Middleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseResetGuid(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Use3Middleware>();
        }
        public static IApplicationBuilder UseShowGuid(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Use2Middleware>();
        }
    }
}
