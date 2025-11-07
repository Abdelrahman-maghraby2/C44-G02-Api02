using E_Commerce.Domain.Contract;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace E_Commerce_Web.Extensions
{
    public static class WebApplicationRegisterantion
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
          await  using var scope = app.Services.CreateAsyncScope();
            var DbContextServes = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var PendingMigrateions = await DbContextServes.Database.GetPendingMigrationsAsync();
         if (PendingMigrateions.Any())
                DbContextServes.Database.Migrate();

            return app;
        }

        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
          await  using var scope = app.Services.CreateAsyncScope();
            var DataIntializerserves = scope.ServiceProvider.GetRequiredService<IDataIntializer>();
          await  DataIntializerserves.IntializeAsync();
            return app;
        }
    }
}
