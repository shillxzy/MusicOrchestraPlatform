using OrderService.BLL.DTOs.Order;
using OrderService.BLL.Services.Interfaces;
using OrderService.DAL.Repositories.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Exceptions;
using AutoMapper;
using OrderService.BLL.DTOs.Customer;
using OrderService.BLL.DTOs.Product;
using OrderService.BLL.DTOs.OrderItem;

namespace OrderService.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IOrderItemRepository orderItemRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return null;

            var orderDto = _mapper.Map<OrderDto>(order);
            
            // Load customer
            var customer = await _customerRepository.GetByIdAsync(order.CustomerId);
            orderDto.Customer = customer != null ? _mapper.Map<CustomerDto>(customer) : null;

            // Load order items
            var orderItems = await _orderItemRepository.GetByOrderIdAsync(id);
            orderDto.OrderItems = _mapper.Map<List<OrderItemDto>>(orderItems);

            // Load products for each order item
            foreach (var orderItem in orderDto.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
                orderItem.Product = product != null ? _mapper.Map<ProductDto>(product) : null;
            }

            return orderDto;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            var orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                var orderDto = _mapper.Map<OrderDto>(order);
                
                // Load customer
                var customer = await _customerRepository.GetByIdAsync(order.CustomerId);
                orderDto.Customer = customer != null ? _mapper.Map<CustomerDto>(customer) : null;

                // Load order items
                var orderItems = await _orderItemRepository.GetByOrderIdAsync(order.Id);
                orderDto.OrderItems = _mapper.Map<List<OrderItemDto>>(orderItems);

                // Load products for each order item
                foreach (var orderItem in orderDto.OrderItems)
                {
                    var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
                    orderItem.Product = product != null ? _mapper.Map<ProductDto>(product) : null;
                }

                orderDtos.Add(orderDto);
            }

            return orderDtos;
        }

        public async Task<OrderDto> CreateAsync(OrderCreate createDto)
        {
            // Validate customer exists
            var customer = await _customerRepository.GetByIdAsync(createDto.CustomerId);
            if (customer == null)
                throw new NotFoundException($"Customer with ID {createDto.CustomerId} not found");

            if (createDto.OrderItems == null || !createDto.OrderItems.Any())
                throw new ValidationException("Order must have at least one item");

            // Validate all products exist and calculate total
            decimal totalAmount = 0;
            foreach (var item in createDto.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new NotFoundException($"Product with ID {item.ProductId} not found");

                if (item.Quantity <= 0)
                    throw new ValidationException($"Quantity must be greater than zero for product {product.Name}");

                if (item.UnitPrice <= 0)
                    throw new ValidationException($"Unit price must be greater than zero for product {product.Name}");

                totalAmount += item.Quantity * item.UnitPrice;
            }

            // Create order
            var order = new Order
            {
                CustomerId = createDto.CustomerId,
                TotalAmount = totalAmount,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            var orderId = await _orderRepository.AddAsync(order);
            order.Id = orderId;

            // Create order items
            foreach (var itemDto in createDto.OrderItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = orderId,
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice
                };

                await _orderItemRepository.AddAsync(orderItem);
            }

            return await GetByIdAsync(orderId);
        }

        public async Task<OrderDto> UpdateAsync(OrderUpdate updateDto)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(updateDto.Id);
            if (existingOrder == null)
                throw new NotFoundException($"Order with ID {updateDto.Id} not found");

            // Validate customer exists
            var customer = await _customerRepository.GetByIdAsync(updateDto.CustomerId);
            if (customer == null)
                throw new NotFoundException($"Customer with ID {updateDto.CustomerId} not found");

            var order = _mapper.Map<Order>(updateDto);
            await _orderRepository.UpdateAsync(order);

            return await GetByIdAsync(updateDto.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new NotFoundException($"Order with ID {id} not found");

            // Delete order items first
            var orderItems = await _orderItemRepository.GetByOrderIdAsync(id);
            foreach (var item in orderItems)
            {
                await _orderItemRepository.DeleteAsync(item.Id);
            }

            // Delete order
            await _orderRepository.DeleteAsync(id);
        }
    }
}
