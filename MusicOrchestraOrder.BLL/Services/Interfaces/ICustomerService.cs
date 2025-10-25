using OrderService.BLL.DTOs.Customer;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto> CreateAsync(CustomerCreate createDto);
        Task<CustomerDto> UpdateAsync(CustomerUpdate updateDto);
        Task DeleteAsync(int id);
    }
}
