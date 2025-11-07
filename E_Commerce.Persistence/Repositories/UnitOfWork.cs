using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entites;
using E_Commerce.Persistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = [];
        public UnitOfWork(StoreDbContext dbContext)
        {
         _dbContext = dbContext;
        }
        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           var EntityType = typeof(TEntity);

            if (_repositories.TryGetValue(EntityType, out object? repositories))
                return (IGenaricRepository<TEntity, TKey>)repositories;

            var NewRepo = new GenaricRepository<TEntity, TKey>(_dbContext);
            _repositories[EntityType] = NewRepo;
            return NewRepo;
        }

        public async Task<int> SaveChangesAsync()=> await _dbContext.SaveChangesAsync();
    }
}
