using OrderService.BLL.DTOs.Product;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto?> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> CreateAsync(ProductCreate createDto);
        Task<ProductDto> UpdateAsync(ProductUpdate updateDto);
        Task DeleteAsync(int id);
    }
}
