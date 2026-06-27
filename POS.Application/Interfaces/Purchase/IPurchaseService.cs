using POS.Application.DTOs.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Interfaces.Purchase
{
    public interface IPurchaseService
    {
        Task CreatePurchase(CreatePurchaseDto dto);
        Task<List<PurchaseDto>> GetAll();
        Task<PurchaseDetailsDto?> GetById(int id);
    }
}
