﻿using Shared.DTO_S.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_S.OrderDtos
{
    public class OrderToReturnDto
    {      
        public Guid Id { get; set; } 
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } 
        public string OrderStatus { get; set; }= default!;
        public AddressDto Address { get; set; } = default!;
        public string DeliveryMethodName { get; set; } = default!;
        public ICollection<OrderItemDto> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
