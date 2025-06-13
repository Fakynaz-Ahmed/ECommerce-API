using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DTO_S;
using Shared.DTO_S.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    // baseURL / api / product
    public class ProductController(IServiceManager _serviceManager) : ApiBaseController
    {
        #region get all products
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProductsAsync([FromQuery]ProductQueryParams productQueryParams)
        {
            var ProductsDto = await _serviceManager.ProductServices.GetAllProducts(productQueryParams);
            return Ok(ProductsDto);
        }
        #endregion

        #region get product by Id
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int Id)
        {
            var ProductDto = await _serviceManager.ProductServices.GetProductById(Id);
            return Ok(ProductDto);
        }
        #endregion

        #region get all brands

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrandsAsync()
        {
            var BrandsDto = await _serviceManager.ProductServices.GetAllBrands();
            return Ok(BrandsDto);
        }
        #endregion

        #region get all types

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypesAsync()
        {
            var TypesDto = await _serviceManager.ProductServices.GetAllTypes();
            return Ok(TypesDto);
        } 
        #endregion
    }
}
