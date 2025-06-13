using AutoMapper;
using E_Commerce.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared.DTO_S.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profiles
{
    internal class ProductPictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;

            destination.PictureUrl = $"{_configuration.GetSection("Urls")[ "ImageBaseUrl"]}{source.PictureUrl}";
            return destination.PictureUrl;
        }
    }
}
