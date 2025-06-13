using AutoMapper;
using E_Commerce.Web.Models;
using Shared.DTO_S;
using Shared.DTO_S.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                     .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                     .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.ProductType.Name))
                     .ForMember(dist => dist.PictureUrl, options => options.MapFrom<ProductPictureUrlResolver>());
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
