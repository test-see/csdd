using foundation.config;
using foundation.exception;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace csdd.Middlewares
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiResponseMiddleware> _logger;

        public ApiResponseMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ApiResponseMiddleware>();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Path: {context.Request.Path}. Message: {ex.Message}");
                var code = (ex as DefaultException)?.StatusCode ?? (int)HttpStatusCode.BadRequest;
                var response = new OkMessage<string>(code, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                var data = JsonConvert.SerializeObject(response, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                await context.Response.WriteAsync(data);
            }
        }
    }
}
