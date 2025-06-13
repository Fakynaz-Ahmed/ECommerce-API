using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DTO_S.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BasketService(IBasketRepository _basketRepository , IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto Basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(Basket);
            var CreatedOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket, TimeSpan.FromDays(60));
            if (CreatedOrUpdatedBasket is null)
            {
                throw new Exception("Can Not Perform This Proccess Now, Try Again Later");
            }
            return _mapper.Map<CustomerBasket,BasketDto>(CreatedOrUpdatedBasket);
        }

        public async Task<bool> DeleteBasketAsync(string Key) =>await _basketRepository.DeleteBasketAsync(Key);


        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var Basket =await _basketRepository.GetBasketAsync(key);
            if (Basket is null)
            {
                throw new BasketNotFoundException(key);
            }
            return _mapper.Map<CustomerBasket, BasketDto>(Basket);
        }
    }
}
