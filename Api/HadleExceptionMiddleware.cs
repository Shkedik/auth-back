using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class HadleExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HadleExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch
            {
                throw;
            }
        }
    }

    public static class HadleExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHadleExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HadleExceptionMiddleware>();
        }
    }
}
