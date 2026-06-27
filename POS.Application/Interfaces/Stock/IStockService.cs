using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Application.DTOs.Stock;

namespace POS.Application.Interfaces.Stock
{
    public interface IStockService
    {
        Task<List<ProductStockDto>> GetAllStock();

        Task<List<ProductStockDto>> GetLowStock();

        Task<ProductStockDto?> GetProductStock(int productId);
    }
}
