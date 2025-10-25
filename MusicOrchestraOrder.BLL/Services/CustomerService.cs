using OrderService.BLL.DTOs.Customer;
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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return customer != null ? _mapper.Map<CustomerDto>(customer) : null;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> CreateAsync(CustomerCreate createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Name))
                throw new ValidationException("Customer name is required");

            if (string.IsNullOrWhiteSpace(createDto.Email))
                throw new ValidationException("Customer email is required");

            if (!IsValidEmail(createDto.Email))
                throw new ValidationException("Invalid email format");

            var customer = _mapper.Map<Customer>(createDto);
            var customerId = await _customerRepository.AddAsync(customer);
            customer.Id = customerId;

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> UpdateAsync(CustomerUpdate updateDto)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(updateDto.Id);
            if (existingCustomer == null)
                throw new NotFoundException($"Customer with ID {updateDto.Id} not found");

            if (string.IsNullOrWhiteSpace(updateDto.Name))
                throw new ValidationException("Customer name is required");

            if (string.IsNullOrWhiteSpace(updateDto.Email))
                throw new ValidationException("Customer email is required");

            if (!IsValidEmail(updateDto.Email))
                throw new ValidationException("Invalid email format");

            var customer = _mapper.Map<Customer>(updateDto);
            await _customerRepository.UpdateAsync(customer);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                throw new NotFoundException($"Customer with ID {id} not found");

            await _customerRepository.DeleteAsync(id);
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
