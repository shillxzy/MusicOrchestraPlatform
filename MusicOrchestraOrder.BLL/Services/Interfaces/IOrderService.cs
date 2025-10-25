using OrderService.BLL.DTOs.Order;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto?> GetByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> CreateAsync(OrderCreate createDto);
        Task<OrderDto> UpdateAsync(OrderUpdate updateDto);
        Task DeleteAsync(int id);
    }
}
