using foundation.exception;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace csdd.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Path: {httpContext.Request.Path}. Message: {ex.Message}");
                httpContext.Response.ContentType = "application/json";
                var status = ex is DefaultException ? HttpStatusCode.BadRequest : HttpStatusCode.InternalServerError;
                httpContext.Response.StatusCode = (int)status;
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { Status = status, Message = ex.Message }));
            }
        }
    }
}
