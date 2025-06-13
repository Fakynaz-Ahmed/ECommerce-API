using Shared;
using Shared.DTO_S;
using Shared.DTO_S.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductServices
    {
        //Get All Products
        public Task<PaginatedResult<ProductDto>> GetAllProducts(ProductQueryParams productQueryParams);
        //Get Product By Id 
        public Task<ProductDto?> GetProductById(int id);
        //Get All Brands
        public Task<IEnumerable<BrandDto>> GetAllBrands();

        //Grt All Types
        public Task<IEnumerable<TypeDto>> GetAllTypes();

    }
}
