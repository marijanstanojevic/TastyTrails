using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using TastyTrails.API.Models;
using TastyTrails.Application.Common.Exceptions;

namespace TastyTrails.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (RequestValidationException ex)
            {
                await HandleResponseAsync(context, HttpStatusCode.BadRequest, ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleResponseAsync(context);
            }
        }

        private async Task HandleResponseAsync(HttpContext context, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, IDictionary<string, string[]>? errors = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorDetail = new ErrorDetailResponse(statusCode, errors);
            var response = JsonSerializer.Serialize(errorDetail);

            await context.Response.WriteAsync(response);
        }
    }
}
