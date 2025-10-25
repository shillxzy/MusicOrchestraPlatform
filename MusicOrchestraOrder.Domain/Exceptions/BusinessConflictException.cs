﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Exceptions
{
    public class BusinessConflictException : Exception
    {
        public BusinessConflictException(string message) : base(message) { }
    }
}
