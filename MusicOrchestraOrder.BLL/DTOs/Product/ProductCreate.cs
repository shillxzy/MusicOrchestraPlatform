using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.DTOs.Product
{
    public class ProductCreate
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
