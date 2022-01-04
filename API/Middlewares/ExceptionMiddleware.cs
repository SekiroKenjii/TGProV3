using Core.Exceptions;
using Core.Wrappers;
using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                switch (context.Response.StatusCode)
                {
                    case (int)HttpStatusCode.Unauthorized:
                        await HandleUnauthorized(context);
                        break;
                    case (int)HttpStatusCode.Forbidden:
                        await HandleForbidden(context);
                        break;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleException(context, exception);
            }
        }

        private static async Task HandleUnauthorized(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var response = new Response<string>
            {
                StatusCode = context.Response.StatusCode,
                Message = "This resource requires an authenticated user",
                Errors = default,
                Data = default
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }

        private static async Task HandleForbidden(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var response = new Response<string>
            {
                StatusCode = context.Response.StatusCode,
                Message = "You are unauthorized to access this resource",
                Errors = default,
                Data = default
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }

        private static async Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new Response<string>
            {
                Errors = default,
                Data = default
            };

            switch (exception)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = exception.Message;
                    break;
                case BadRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = exception.Message;
                    break;
                case UnauthorizedException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = exception.Message;
                    break;
                case ValidationException e:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = exception.Message;
                    response.Errors = e.Errors;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = exception.Message;
                    break;
            }

            response.StatusCode = context.Response.StatusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
