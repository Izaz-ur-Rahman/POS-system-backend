using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS.Domain.Entities;

namespace POS.Application.Interfaces.Auth
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
