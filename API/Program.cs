using API.Extensions;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddApplicationServices(config);

builder.Services.AddAuthenticationServices(config);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => options.RouteTemplate = "swagger/{documentName}/swagger.json");
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "TGProV3 - API";
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
    });
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseMiddleware<HandleIdentificationFailedMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.MigrateDBAsync();

await app.RunAsync();
