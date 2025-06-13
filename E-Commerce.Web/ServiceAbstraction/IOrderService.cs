using Shared.DTO_S.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        //Create Order
        //Take (orderDto(BasketId,ShippingAddress , DeliveryMethodId) , UserEmail(FromToken))
        //Return OrderDetials (OrderId, username , orderdate,Items(productname-pictureurl-price-quentity),
        //,Address , deliverymenthodNmae , orderstatus , subtotal,total)
        public Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto , string email);

        //Get All DeliveryMethods
        public Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodAsync();

        //Get All Orders For A specific User
        public Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email);

        //Get Order By Id
        public Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id);
    }
}
