using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class BasketNotFoundException(string Key):NotFoundException($"This Basket with Id = {Key} Is Not Found")
    {
    }
}
