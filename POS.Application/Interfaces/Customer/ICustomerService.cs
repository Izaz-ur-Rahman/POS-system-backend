using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS.Application.DTOs.Customer;

namespace POS.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAll();

        Task<CustomerDto?> GetById(int id);

        Task Create(CreateCustomerDto dto);

        Task Update(int id, UpdateCustomerDto dto);

        Task Delete(int id);
    }
}