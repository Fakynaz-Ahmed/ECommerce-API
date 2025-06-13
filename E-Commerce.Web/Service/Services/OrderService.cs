using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.OrderModule;
using E_Commerce.Web.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DTO_S.IdentityDtos;
using Shared.DTO_S.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class OrderService(IMapper _mapper, IUnitOfWork _unitOfWork, IBasketRepository _basketRepository) : IOrderService
    {
        //orderDto(BasketId,ShippingAddress , DeliveryMethodId)
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            //shipping address
            var ShippingAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.ShippingAddress);
            //deliveryMethod
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                                 ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);
            //ICollection<OrderItem> items
            List<OrderItem> OrderItems = [];
            //1.Get Basket 
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId)
                         ?? throw new BasketNotFoundException(orderDto.BasketId);
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var ProductFromDB = await ProductRepo.GetByIdAsync(item.Id)
                                    ?? throw new ProductNotFoundException(item.Id);
                OrderItems.Add(CreateOrderItem(item, ProductFromDB));
            }
            //subTotal
            var SubTotal = OrderItems.Sum(OI => OI.Price * OI.Quentity);
            //Create new object from order
            var Order = new Order(email, ShippingAddress, DeliveryMethod, OrderItems, SubTotal);
            //Add Order In DataBase By using Repository<Order>
            //must Save changes this operation
            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(Order);
            await _unitOfWork.SaveChangesAsync();
            //return OrderToReturnDto after Mapping
            return _mapper.Map<Order , OrderToReturnDto>(Order);
        }

        private static OrderItem CreateOrderItem(BasketItem item, Product ProductFromDB)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrdered()
                {
                    ProductId = ProductFromDB.Id,
                    ProductName = ProductFromDB.Name,
                    PictureUrl = ProductFromDB.PictureUrl,
                },
                Price = ProductFromDB.Price,
                Quentity = item.Quentity,
            };
        }



        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodAsync()
        {
           var DeliveryMethods =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
           var DeliveryMethodsDto = _mapper.Map<IEnumerable<DeliveryMethod> , IEnumerable< DeliveryMethodDto>> (DeliveryMethods);
            return DeliveryMethodsDto;
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email)
        {
            var Specification = new OrderSpecifications(email);
            var Orders =await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Specification);
            //if (Orders is null) throw new Exception("Not Exist Orders Yet!!");
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(Orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id)
        {
            var Specification = new OrderSpecifications(Id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Specification);
            if(Order is null) throw new OrderNotFoundException(Id);
            return _mapper.Map<Order , OrderToReturnDto>(Order);
        }
    }
}
