﻿using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IUnitOfWork
    {
       public IGenericRepository<TEntity,TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
       public Task<int> SaveChangesAsync();
    }
}
