using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using POS.Application.DTOs.Customer;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Data;

namespace POS.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerDto>> GetAll()
        {
            return await _context.Customers
                  .Where(x => !x.IsDeleted)
                .Select(x => new CustomerDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Email = x.Email,
                    Address = x.Address,
                    CreditBalance = x.CreditBalance,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<CustomerDto?> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                CreditBalance = customer.CreditBalance,
                IsActive = customer.IsActive
            };
        }

        public async Task Create(CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, UpdateCustomerDto dto)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null) return;

            customer.Name = dto.Name;
            customer.Phone = dto.Phone;
            customer.Email = dto.Email;
            customer.Address = dto.Address;
            customer.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
        }

        //public async Task Delete(int id)
        //{
        //    var customer = await _context.Customers.FindAsync(id);

        //    if (customer == null) return;

        //    _context.Customers.Remove(customer);
        //    await _context.SaveChangesAsync();
        //}
        public async Task Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null) return;

            customer.IsDeleted = true;
            customer.IsActive = false;

            await _context.SaveChangesAsync();
        }
    }
}
