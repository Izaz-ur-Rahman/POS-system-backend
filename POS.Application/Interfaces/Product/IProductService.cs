using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS.Application.DTOs.Product;

namespace POS.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAll();

        Task<ProductDto?> GetById(int id);

        Task Create(CreateProductDto dto);

        Task Update(int id, UpdateProductDto dto);

        Task Delete(int id);
        Task<List<LowStockProductDto>> GetLowStockProducts();
    }
}
