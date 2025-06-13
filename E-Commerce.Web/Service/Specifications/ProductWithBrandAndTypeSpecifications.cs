using DomainLayer.Models;
using E_Commerce.Web.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        //Not Exist where -> Get All products with types and brands
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams productQueryParams) : base(p => (!productQueryParams.BrandId.HasValue || p.BrandId == productQueryParams.BrandId) && (!productQueryParams.TypeId.HasValue || p.TypeId == productQueryParams.TypeId) && (string.IsNullOrWhiteSpace(productQueryParams.SearchValue)|| p.Name.ToLower().Contains(productQueryParams.SearchValue.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (productQueryParams.SortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    break;
            }

            ApplyPagination(productQueryParams.PageSize,productQueryParams.PageIndex);
        }

        //Exist where -> Get by Condition (By Id)  -> get product by Id
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
