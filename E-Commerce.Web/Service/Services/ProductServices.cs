using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using E_Commerce.Web.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DTO_S;
using Shared.DTO_S.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductServices
    {

        public async Task<PaginatedResult<ProductDto>> GetAllProducts(ProductQueryParams productQueryParams)
        {
            ProductWithBrandAndTypeSpecifications specification = new ProductWithBrandAndTypeSpecifications(productQueryParams);
            var Repo =  _unitOfWork.GetRepository<Product, int>();
            var products = await Repo.GetAllAsync(specification);
            var ProductsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);

            var TotalCountSpec = new ProductCountSpecification(productQueryParams);
            int TotalProducts =await Repo.CountAsync(TotalCountSpec);
            var Result = new PaginatedResult<ProductDto>(productQueryParams.PageIndex, ProductsDto.Count(), TotalProducts, ProductsDto);
            return Result;
        }

        public async Task<ProductDto?> GetProductById(int id)
        {
            ProductWithBrandAndTypeSpecifications specification = new ProductWithBrandAndTypeSpecifications(id);
            var Repo =  _unitOfWork.GetRepository<Product, int>();
            var Product = await Repo.GetByIdAsync(specification);
            if (Product is null)
            {
                throw new ProductNotFoundException(id);
            }
            var ProductDto = _mapper.Map<Product, ProductDto>(Product);
            return ProductDto;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrands()
        {
            var Repo =  _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return BrandsDto;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypes()
        {
            var Repo =  _unitOfWork.GetRepository<ProductType, int>();
            var Types = await Repo.GetAllAsync();
            var TypesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }
    }
}
