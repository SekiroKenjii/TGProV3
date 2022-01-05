using Data;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API.Extensions
{
    public static class MigrationDBExtensions
    {
        public static async Task MigrateDBAsync(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                var defaultCredential = services.GetRequiredService<IOptions<DefaultCredential>>();
                await context.Database.MigrateAsync();
                await SeedData.Execute(context, defaultCredential.Value);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during migration");
            }
        }
    }
}
