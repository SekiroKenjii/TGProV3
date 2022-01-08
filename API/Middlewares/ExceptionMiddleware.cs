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
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleException(context, exception);
            }
        }

        private static async Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new Response<string>
            {
                Errors = new List<string>(),
                Data = default
            };

            switch (exception)
            {
                case NotFoundException notFound:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = HttpStatusCode.NotFound.ToString();
                    response.Errors = notFound.Errors;
                    break;
                case BadRequestException badRequest:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = HttpStatusCode.BadRequest.ToString();
                    response.Errors = badRequest.Errors;
                    break;
                case UnauthorizedException unauthorized:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = HttpStatusCode.Unauthorized.ToString();
                    response.Errors = unauthorized.Errors;
                    break;
                case ValidationException validation:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = HttpStatusCode.BadRequest.ToString();
                    response.Errors = validation.Errors;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = HttpStatusCode.InternalServerError.ToString();
                    response.Errors.Append(exception.Message);
                    break;
            }

            response.StatusCode = context.Response.StatusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
