using Microsoft.EntityFrameworkCore;
using Storage.Persistence;
using Storage.Persistence.Data;

namespace Storage.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<StorageContext>();

                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();

                try
                {
                    await SeedData.InitAsync(context);
                }
                catch (Exception ex) {
                    throw;
                }
            }
        }
    }
}
