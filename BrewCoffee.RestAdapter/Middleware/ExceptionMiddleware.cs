using BrewCoffee.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BrewCoffee.RestAdapter.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex is OutOfCoffeeException)
                {
                    await HandleOutOfCoffeeExceptionAsync(httpContext, ex);
                }
                else
                {
                    await HandleExceptionAsync(httpContext, ex);
                }
            }
        }

        private async Task HandleOutOfCoffeeExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            await context.Response.Body.FlushAsync();
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails()
            {
                Status = context.Response.StatusCode,
                Detail = exception.Message ?? "Internal Server Error."
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, settings: _jsonSerializerSettings));
        }
    }
}
