using Application.Common.Interfaces.Services;
using Application.Common.Models.GlobalErrorHandling;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Middleware
{
    class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogService logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _next = next;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //ToDo: Add Authenticated User 
                var exceptionId = Guid.NewGuid();
                _logger.LogError($"ErrorId:{exceptionId}|Method:{httpContext.Request.Method} Endpoint:{httpContext.Request.Path}|{ex.InnerException}|{Environment.NewLine}{ex.StackTrace}");
                await HandleExceptionAsync(httpContext, ex, exceptionId);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception, Guid id)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ExceptionDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = _env.IsDevelopment() ? exception.ToString() : $"Internal Server Error:{id}"
            }.ToString());
        }
    }
}
