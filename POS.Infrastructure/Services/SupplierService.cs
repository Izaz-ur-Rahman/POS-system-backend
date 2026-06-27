using Microsoft.EntityFrameworkCore;
using POS.Application.DTOs.Supplier;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Data;

namespace POS.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly AppDbContext _context;

        public SupplierService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SupplierDto>> GetAll()
        {
            return await _context.Suppliers
                .Select(x => new SupplierDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Email = x.Email,
                    Address = x.Address,
                    CompanyName = x.CompanyName,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<SupplierDto?> GetById(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null) return null;

            return new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Address = supplier.Address,
                CompanyName = supplier.CompanyName,
                IsActive = supplier.IsActive
            };
        }

        public async Task Create(CreateSupplierDto dto)
        {
            var supplier = new Supplier
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                CompanyName = dto.CompanyName
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, UpdateSupplierDto dto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null) return;

            supplier.Name = dto.Name;
            supplier.Phone = dto.Phone;
            supplier.Email = dto.Email;
            supplier.Address = dto.Address;
            supplier.CompanyName = dto.CompanyName;
            supplier.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null) return;

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }
    }
}