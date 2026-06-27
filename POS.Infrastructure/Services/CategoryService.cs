using Microsoft.EntityFrameworkCore;
using POS.Application.DTOs.Category;
using POS.Application.Interfaces;
using POS.Infrastructure.Data;
using POS.Domain.Entities;

namespace POS.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            return await _context.Categories
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<CategoryDto?> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                IsActive = category.IsActive
            };
        }

        public async Task Create(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null) return;

            category.Name = dto.Name;
            category.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null) return;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}