using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Application.DTOs.Inventory;

namespace POS.Application.Interfaces.Inventory
{
    public interface IInventoryService
    {
        Task<List<StockMovementDto>>
            GetStockMovement(int productId);

        Task<InventoryValuationDto> GetInventoryValuation();
        Task<List<LowStockDto>> GetLowStockProducts();
        Task<ProfitSummaryDto> GetProfitSummary();
        Task<List<DailyProfitDto>> GetDailyProfit();
        Task<object> GetProfitMargin();
        Task<List<ProductProfitDto>> GetProductProfitReport();
        Task<List<StockLedgerDto>> GetStockLedger(int productId);
        Task<List<LowStockAlertDto>> GetReorderAlerts();
    }
}
