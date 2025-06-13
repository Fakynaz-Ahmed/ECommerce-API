using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class OrderNotFoundException(Guid Id) : NotFoundException($"Not Found Order With Id = {Id} ")
    {
    }
}
