﻿using AutoMapper;
using DomainLayer.Models.IdentityModule;
using Shared.DTO_S.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profiles
{
     class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}
