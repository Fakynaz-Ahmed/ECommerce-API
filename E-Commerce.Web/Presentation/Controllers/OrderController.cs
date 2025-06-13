using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTO_S.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrderController(IServiceManager _serviceManager) : ApiBaseController
    {
        //craete order 
        [HttpPost] //POST baseUrl/api/Order
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);//Get Email From Token
            var Result =await _serviceManager.OrderService.CreateOrderAsync(orderDto, Email!);
            return Ok(Result);
        }

        //GET All DeliveryMethods
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetAllDeliveryMethods()
        {
            var DeliveryMethods =await _serviceManager.OrderService.GetAllDeliveryMethodAsync();
            return Ok(DeliveryMethods);
        }

        //Get All Orders For A specific User
        [HttpGet("AllOrders")]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Orders =await _serviceManager.OrderService.GetAllOrdersAsync(Email!);
            return Ok(Orders);
        }

        //Get Order By Id
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var Order =await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(Order);
        }
    }
}
