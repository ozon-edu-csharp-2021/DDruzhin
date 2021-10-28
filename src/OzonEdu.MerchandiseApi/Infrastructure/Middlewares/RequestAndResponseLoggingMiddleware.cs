using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OzonEdu.MerchandiseApi.Infrastructure.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context);
            await _next(context);
            await LogResponse(context);
        }

        private async Task LogResponse(HttpContext context)
        {
            try
            {
                if (context.Request.ContentType is "application/grpc" or "application/grpc-web-text")
                {
                    return;
                }
                var logInfo = "\n";
                logInfo += $"Response to {context.Request.Host}{context.Request.Path} logged\n";

                logInfo += "Headers:\n";
                if (context.Response.Headers.Count > 0)
                {
                    foreach (var (key, value) in context.Response.Headers)
                    {
                        logInfo += $"   {key} = {value}\n";
                    }
                }
                else
                {
                    logInfo += "    no headers\n";
                }

                _logger.LogInformation(logInfo);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log response");
            }
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                if (context.Request.ContentType is "application/grpc" or "application/grpc-web-text")
                {
                    return;
                }
                var logInfo = "\n";
                logInfo += $"Request for {context.Request.Host}{context.Request.Path} logged\n";

                logInfo += "Headers:\n";
                if (context.Request.Headers.Count > 0)
                {
                    foreach (var (key, value) in context.Request.Headers)
                    {
                        logInfo += $"   {key} = {value}\n";
                    }
                }
                else
                {
                    logInfo += "    no headers";
                }

                _logger.LogInformation(logInfo);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request");
            }
        }
    }
}