using DomainLayer.Contracts;
using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    class OrderSpecifications : BaseSpecifications<Order , Guid>
    {
        //Get All Orders For A specific User
        public OrderSpecifications(string Email):base(O => O.UserEmail == Email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDesc(O=>O.OrderDate);
        }

        //Get Order By Id
        public OrderSpecifications(Guid Id) : base(O=>O.Id== Id)
        {
            AddInclude(O=>O.DeliveryMethod);
            AddInclude(O => O.Items);
        }

    }
}
