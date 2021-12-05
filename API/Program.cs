using API.Extensions;
using API.Middlewares;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.

builder.Services.AddApplicationServices(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//using var scope = app.Services.CreateScope();

//var services = scope.ServiceProvider;

//try
//{
//    var context = services.GetRequiredService<DataContext>();
//    var defaultCredential = services.GetRequiredService<IOptions<DefaultCredential>>();
//    await context.Database.MigrateAsync();
//    await Seed.SeedDefaultUser(context, defaultCredential.Value);
//}
//catch (Exception ex)
//{
//    var logger = services.GetRequiredService<ILogger<Program>>();
//    logger.LogError(ex, "An error occured during migration");
//}

await app.RunAsync();
