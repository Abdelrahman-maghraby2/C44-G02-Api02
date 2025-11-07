
using E_Commerce.Domain.Contract;
using E_Commerce.Persistence.Data.DataSeeding;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Serves;
using E_Commerce.Serves.MappingProfiles;
using E_Commerce.Serves_Abstraction;
using E_Commerce_Web.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce_Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDataIntializer, DataIntializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductServes, ProductServes>();
            //builder.Services.AddAutoMapper(x => x.AddProfile<ProductProfile>());
            //builder.Services.AddAutoMapper(x=>x.LicenseKey="",typeof(ProductProfile).Assembly); 
            builder.Services.AddAutoMapper(typeof(ServesAssemblyReference).Assembly);
            builder.Services.AddTransient<ProductPictureUrlResolver>();


            #endregion

            var app = builder.Build();

            #region DataSeeding - Apply Migurations
             await  app.MigrateDatabaseAsync();
             await  app.SeedDatabaseAsync();
       

            #endregion

            #region  Configure the HTTP request pipeline.

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllers();
            #endregion

            await app.RunAsync();
        }
    }
}
