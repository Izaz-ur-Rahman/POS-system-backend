using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Application.DTOs.Auth;
using POS.Application.Interfaces.Auth;
using POS.Infrastructure.Data;
using POS.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                Role = "Admin"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User created successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                return Unauthorized("Invalid email");

            if (user.PasswordHash != HashPassword(dto.Password))
                return Unauthorized("Invalid password");

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                user = user.FullName,
                role = user.Role
            });
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}