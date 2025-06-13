using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            //EF Core requires a parameterless constructor to be able to instantiate the entity when loading data from the database
        }
        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod,  ICollection<OrderItem> items, decimal subTotal)
        {
            UserEmail = userEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string UserEmail { get; set; } = default!;
        public OrderAddress Address { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;//Navigation Property
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; } //price*Quentity


        #region Have DefaultValues,FK,Total
        //Have DefaultValues
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; }

        //FK
        public int DeliveryMethodId { get; set; } // FK

        //Total
        //[NotMapped]
        //public decimal Total { get => SubTotal + DeliveryMethod.Price; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Price; 
        #endregion

    }
}
