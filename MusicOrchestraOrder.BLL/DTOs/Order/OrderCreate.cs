using OrderService.BLL.DTOs.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.DTOs.Order
{
    public class OrderCreate
    {
        public int CustomerId { get; set; }
        public List<OrderItemCreate> OrderItems { get; set; } = new();
    }
}

