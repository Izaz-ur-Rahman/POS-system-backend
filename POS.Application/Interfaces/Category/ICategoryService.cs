using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS.Application.DTOs.Category;

namespace POS.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto?> GetById(int id);
        Task Create(CreateCategoryDto dto);
        Task Update(int id, UpdateCategoryDto dto);
        Task Delete(int id);
    }
}
