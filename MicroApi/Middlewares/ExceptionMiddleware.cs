using System;
using System.Threading.Tasks;
using MicroApi.Handlers;
using Microsoft.AspNetCore.Http;

namespace MicroApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        
        public async Task InvokeAsync(HttpContext context, ExceptionHandler exceptionHandler)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await exceptionHandler.ProcessException(e);
            }
        }
    }
}