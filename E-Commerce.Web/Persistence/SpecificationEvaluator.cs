using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    internal static class SpecificationEvaluator
    {
        //create query
        //_dbcontext.products.where(p=>p.id==id).include(p=>p.productbrand).include(p=>p.producttype)
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery , ISpecifications<TEntity , TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            //start build query 
            var Query = InputQuery;

            #region where

            if (specifications.Criteria is not null)
            {
                Query = Query.Where(specifications.Criteria);
            }

            #endregion

            #region Sorting
            if (specifications.OrderBy is not null)
            {
                Query = Query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(specifications.OrderByDescending);
            }
            #endregion

            #region Include
            var IncludeExpressionsCount = specifications.IncludeExpressions.Count;
            if (specifications.IncludeExpressions is not null && IncludeExpressionsCount > 0)
            {
                //foreach (var IncludeExpression in specifications.IncludeExpressions)                
                //{
                //    Query=Query.Include(IncludeExpression);
                //}

                /*                    Another Way                   */
                //_dbcontext.products.where()
                //_dbcontext.products.where().Include()
                //_dbcontext.products.where().Include().Include
                Query = specifications.IncludeExpressions.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
            }
            #endregion

            #region Pagination
            if (specifications.IsPaginated)
            {
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);//SKIP first, then TAKE 
            }
            #endregion

            return Query;

        }

    }
}
