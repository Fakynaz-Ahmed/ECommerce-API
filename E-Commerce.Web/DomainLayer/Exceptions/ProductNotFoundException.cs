﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class ProductNotFoundException(int Id) : NotFoundException($"Product With Id = {Id} Is Not Found ")
    {
    }
}
