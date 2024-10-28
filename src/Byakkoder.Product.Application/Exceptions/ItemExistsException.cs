﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byakkoder.Product.Application.Exceptions
{
    public class ItemExistsException : Exception
    {
        public ItemExistsException(string message) : base(message) { }
    }
}
