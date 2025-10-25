using OrderService.BLL.DTOs.Product;
using OrderService.BLL.Services.Interfaces;
using OrderService.DAL.Repositories.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Exceptions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product != null ? _mapper.Map<ProductDto>(product) : null;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> CreateAsync(ProductCreate createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Name))
                throw new ValidationException("Product name is required");

            if (createDto.Price <= 0)
                throw new ValidationException("Product price must be greater than zero");

            var product = _mapper.Map<Product>(createDto);
            var productId = await _productRepository.AddAsync(product);
            product.Id = productId;

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateAsync(ProductUpdate updateDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(updateDto.Id);
            if (existingProduct == null)
                throw new NotFoundException($"Product with ID {updateDto.Id} not found");

            if (string.IsNullOrWhiteSpace(updateDto.Name))
                throw new ValidationException("Product name is required");

            if (updateDto.Price <= 0)
                throw new ValidationException("Product price must be greater than zero");

            var product = _mapper.Map<Product>(updateDto);
            await _productRepository.UpdateAsync(product);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException($"Product with ID {id} not found");

            await _productRepository.DeleteAsync(id);
        }
    }
}
