using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Application.DTOs;
using POS.Application.DTOs.Supplier;

namespace POS.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<List<SupplierDto>> GetAll();

        Task<SupplierDto?> GetById(int id);

        Task Create(CreateSupplierDto dto);

        Task Update(int id, UpdateSupplierDto dto);

        Task Delete(int id);
    }
}