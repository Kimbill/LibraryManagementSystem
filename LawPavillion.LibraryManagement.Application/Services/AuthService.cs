using LawPavillion.LibraryManagement.Application.Interfaces;
using LawPavillion.LibraryManagement.Domain.Entities;
using LawPavillion.LibraryManagement.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LawPavillion.LibraryManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthService> _logger;

        public AuthService(AppDbContext context, IConfiguration config, ILogger<AuthService> logger)
        {
            _context = context;
            _config = config;
            _logger = logger;
        }

        public async Task RegisterAsync(string email, string password)
        {
            _logger.LogInformation("Registering user with email {Email}", email);

            // email must be unique
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                _logger.LogWarning("Registration failed. Email already exists: {Email}", email);
                throw new InvalidOperationException("Email already registered");
            }

            using var hmac = new HMACSHA512();
            var user = new User
            {
                Email = email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User registered successfully: {Email}", email);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            _logger.LogInformation("Login attempt for {Email}", email);

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email)
                ?? throw new UnauthorizedAccessException("Invalid credentials");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            if (!computed.SequenceEqual(user.PasswordHash))
            {
                _logger.LogWarning("Invalid password for {Email}", email);
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            _logger.LogInformation("Login successful for {Email}", email);
            return GenerateJwt(user);
        }

        private string GenerateJwt(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email)};

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_config["Jwt:ExpiryMinutes"]!)),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
