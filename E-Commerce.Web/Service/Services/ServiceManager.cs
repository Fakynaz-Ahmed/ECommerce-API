using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper , IBasketRepository _basketRepository , UserManager<ApplicationUser> _userManager , IConfiguration _configuration) : IServiceManager
    {
        private readonly Lazy<IProductServices> _services = new Lazy<IProductServices>(() => new ProductServices(_unitOfWork, _mapper));
        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _configuration,_mapper));
        private readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(_mapper , _unitOfWork , _basketRepository));
        public IProductServices ProductServices =>_services.Value;
        public IBasketService BasketService => _LazyBasketService.Value;
        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;

        public IOrderService OrderService => _LazyOrderService.Value;
    }
}
