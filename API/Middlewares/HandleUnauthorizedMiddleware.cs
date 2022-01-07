using Core.Constants;
using Core.Wrappers;
using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
    public class HandleIdentificationFailedMiddleware
    {
        private readonly RequestDelegate _next;
        public HandleIdentificationFailedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if(context.Response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                context.Response.ContentType = "application/json";

                var response = new Response<string>
                {
                    StatusCode = context.Response.StatusCode,
                    Errors = default,
                    Data = default
                };

                response.Message = context.Response.StatusCode switch
                {
                    (int)HttpStatusCode.Unauthorized => Errors.AUTHENTICATION_REQUIRED,
                    (int)HttpStatusCode.Forbidden => Errors.AUTHORIZATION_REQUIRED,
                    _ => Errors.AUTHENTICATION_REQUIRED
                };

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
