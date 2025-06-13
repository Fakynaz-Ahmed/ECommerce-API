using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Criteria
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        public BaseSpecifications(Expression<Func<TEntity, bool>>? CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
        #endregion

        #region IncludeExpressions
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; private set; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) => IncludeExpressions.Add(includeExpression);

        #endregion

        #region Order By Ascending & Desending
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderByDesc(Expression<Func<TEntity, object>> OrderByDescExpression)
        {
            OrderByDescending = OrderByDescExpression;
        }
        #endregion

        #region Pagination
        public int Take {  get; private set; }

        public int Skip {  get; private set; }

        public bool IsPaginated { get ; set ; }

       protected void ApplyPagination(int PageSize, int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = PageSize * (PageIndex-1);
       }

        #endregion
    }
}
