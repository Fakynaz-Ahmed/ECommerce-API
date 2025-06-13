using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        #region Get Repository 
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var TypeName = typeof(TEntity).Name;
            #region there is already object in the Dictionary
            if (_repositories.TryGetValue(TypeName, out object? Value))
                return (IGenericRepository<TEntity, TKey>)Value;
            #endregion

            #region Create New Object and Store It In The Dictionary 
            var repo = new GenericRepository<TEntity, TKey>(_dbContext);
            //_repositories.Add(TypeName, repo);
            _repositories["TypeName"] = repo;
            return repo; 
            #endregion
        } 
        #endregion

        #region Save Change
        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
        
        #endregion   
    }
}