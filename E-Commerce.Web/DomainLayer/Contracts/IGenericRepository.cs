using DomainLayer.Models;
using E_Commerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        //Get All Products 
        public Task<IEnumerable<TEntity>> GetAllAsync();
        //Get product By Id
        public Task<TEntity?> GetByIdAsync(TKey id);
        public Task AddAsync(TEntity entity);
        public void Update(TEntity entity);
        public void Remove(TEntity entity);

        #region With Specifications 
        //Get All Products 
        public Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity , TKey> specifications);
        //Get product By Id
        public Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
        //get all and count them 
        public  Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);

        #endregion


    }
}
