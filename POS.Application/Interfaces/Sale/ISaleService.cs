using POS.Application.DTOs.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Interfaces.Sale
{
    public interface ISaleService
    {
        Task CreateSale(CreateSaleDto dto);
        Task<List<SaleDto>> GetAll();
        Task<SaleDetailDto> GetById(int id);
    }
}