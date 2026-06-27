using Microsoft.IdentityModel.Tokens;
using POS.Application.Interfaces.Auth;
using POS.Domain.Entities;
//using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POS.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("THIS_IS_MY_SUPER_SECRET_KEY_12345")
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}