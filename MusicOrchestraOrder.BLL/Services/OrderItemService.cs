using OrderService.BLL.DTOs.OrderItem;
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
using OrderService.BLL.DTOs.Product;

namespace OrderService.BLL.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderItemService(
            IOrderItemRepository orderItemRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<OrderItemDto?> GetByIdAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null) return null;

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);
            
            // Load product
            var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
            orderItemDto.Product = product != null ? _mapper.Map<ProductDto>(product) : null;

            return orderItemDto;
        }

        public async Task<IEnumerable<OrderItemDto>> GetByOrderIdAsync(int orderId)
        {
            var orderItems = await _orderItemRepository.GetByOrderIdAsync(orderId);
            var orderItemDtos = new List<OrderItemDto>();

            foreach (var orderItem in orderItems)
            {
                var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);
                
                // Load product
                var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
                orderItemDto.Product = product != null ? _mapper.Map<ProductDto>(product) : null;

                orderItemDtos.Add(orderItemDto);
            }

            return orderItemDtos;
        }

        public async Task<OrderItemDto> CreateAsync(OrderItemCreate createDto)
        {
            // Validate order exists
            var order = await _orderRepository.GetByIdAsync(createDto.OrderId);
            if (order == null)
                throw new NotFoundException($"Order with ID {createDto.OrderId} not found");

            // Validate product exists
            var product = await _productRepository.GetByIdAsync(createDto.ProductId);
            if (product == null)
                throw new NotFoundException($"Product with ID {createDto.ProductId} not found");

            if (createDto.Quantity <= 0)
                throw new ValidationException("Quantity must be greater than zero");

            if (createDto.UnitPrice <= 0)
                throw new ValidationException("Unit price must be greater than zero");

            var orderItem = _mapper.Map<OrderItem>(createDto);
            var orderItemId = await _orderItemRepository.AddAsync(orderItem);
            orderItem.Id = orderItemId;

            return await GetByIdAsync(orderItemId);
        }

        public async Task<OrderItemDto> UpdateAsync(OrderItemUpdate updateDto)
        {
            var existingOrderItem = await _orderItemRepository.GetByIdAsync(updateDto.Id);
            if (existingOrderItem == null)
                throw new NotFoundException($"Order item with ID {updateDto.Id} not found");

            // Validate order exists
            var order = await _orderRepository.GetByIdAsync(updateDto.OrderId);
            if (order == null)
                throw new NotFoundException($"Order with ID {updateDto.OrderId} not found");

            // Validate product exists
            var product = await _productRepository.GetByIdAsync(updateDto.ProductId);
            if (product == null)
                throw new NotFoundException($"Product with ID {updateDto.ProductId} not found");

            if (updateDto.Quantity <= 0)
                throw new ValidationException("Quantity must be greater than zero");

            if (updateDto.UnitPrice <= 0)
                throw new ValidationException("Unit price must be greater than zero");

            var orderItem = _mapper.Map<OrderItem>(updateDto);
            await _orderItemRepository.UpdateAsync(orderItem);

            return await GetByIdAsync(updateDto.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
                throw new NotFoundException($"Order item with ID {id} not found");

            await _orderItemRepository.DeleteAsync(id);
        }
    }
}
