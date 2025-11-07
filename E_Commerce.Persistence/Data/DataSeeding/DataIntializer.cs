using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entites;
using E_Commerce.Domain.Entites.ProductModule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeeding
{
    public class DataIntializer : IDataIntializer
    {
        private readonly StoreDbContext _dbContext;

        public DataIntializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task IntializeAsync()
        {
            try 
            {
                var HasProduct =await _dbContext.Product.AnyAsync();
                var HasBrands =await _dbContext.ProductBrands.AnyAsync();
                var HasTypes =await _dbContext.ProductTypes.AnyAsync();

                if (HasProduct &&HasTypes &&HasBrands) return;

                if (!HasBrands) 
                {
                  await  SeedDataFromJsonAsync<ProductBrand,int>("brands.json",_dbContext.ProductBrands);
                }
                if (!HasTypes) {
                  await  SeedDataFromJsonAsync<ProductType,int>("types.json", _dbContext.ProductTypes);
                }
                 await _dbContext.SaveChangesAsync();
                if (!HasProduct) {
                   await SeedDataFromJsonAsync<Product,int>("products.json", _dbContext.Product);
                }
                  await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex )
            {
                Console.WriteLine($"data seed is faild{ex}" );
            }
        }

        private async Task  SeedDataFromJsonAsync<T, Tkey>(string FileName,DbSet<T>dbset)where T:BaseEntity<Tkey> 
        {
            //D:\API\Project\E Commerce,Web Solution\E_Commerce.Persistence\Data\DataSeeding\JSONFile\

            var FilePath = @"..\E_Commerce.Persistence\Data\DataSeeding\JSONFile\" + FileName;
            if (!File.Exists(FilePath)) throw new Exception($"{FileName} is not exist");

            try 
            {
               using var datastream = File.OpenRead(FilePath);
               
                var data  =await JsonSerializer.DeserializeAsync<List<T>>(datastream,new JsonSerializerOptions()
                { PropertyNameCaseInsensitive=true});

                if(data != null) await dbset.AddRangeAsync(data) ;
            } 
            catch(Exception ex) 
            { 
                Console.WriteLine($"error where u reading jsonfile : {ex} ");
                return;
            }
        }
    }
}
