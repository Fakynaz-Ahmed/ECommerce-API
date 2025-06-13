using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DTO_S.IdentityDtos;
using Shared.DTO_S.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profiles
{
    public class OrderProfile :Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                     .ForMember(D => D.DeliveryMethodName, Options => Options.MapFrom(S => S.DeliveryMethod.ShortName));
            CreateMap<OrderItem, OrderItemDto>()
                     .ForMember(D => D.ProductName, Options => Options.MapFrom(S => S.Product.ProductName))
                     .ForMember(D => D.PictureUrl, Options => Options.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDto>();
           
        }
    }
}
