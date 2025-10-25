using OrderService.BLL.DTOs.OrderItem;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemDto?> GetByIdAsync(int id);
        Task<IEnumerable<OrderItemDto>> GetByOrderIdAsync(int orderId);
        Task<OrderItemDto> CreateAsync(OrderItemCreate createDto);
        Task<OrderItemDto> UpdateAsync(OrderItemUpdate updateDto);
        Task DeleteAsync(int id);
    }
}
