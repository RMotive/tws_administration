using System;
using System.Globalization;
using System.Net;

namespace Server.Middlewares
{
    public class ExceptionCatcherMiddleware
    {
        private readonly RequestDelegate _delegate;

        public ExceptionCatcherMiddleware(RequestDelegate delegator)
        {
            _delegate = delegator;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _delegate(context);
            }
            catch
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("Es un testeo");
            }
        }
    }
}

