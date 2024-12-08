using blog_api.Data;
using blog_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using blog_api.Exceptions;

namespace blog_api.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TokenService _tokenService;

        public UserService(ApplicationDbContext dbContext, TokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public async Task<TokenResponse> RegisterUser(UserRegisterModel model)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Email == model.Email))
                throw new EmailException(model.Email);
            
            var user = new User
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                FullName = model.FullName,
                Password = HashPassword(model.Password),
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            
            var token = _tokenService.GenerateJwtToken(user.Id);
            
            return new TokenResponse { Token = token };
        }

        private static string HashPassword(string password)
        {
            using var hmac = new HMACSHA256();

            var salt = hmac.Key;
            
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordWithSalt = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, passwordWithSalt, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, passwordWithSalt, passwordBytes.Length, salt.Length);

            var hash = hmac.ComputeHash(passwordWithSalt);
            
            return Convert.ToBase64String(salt.Concat(hash).ToArray());
        }
    }
}