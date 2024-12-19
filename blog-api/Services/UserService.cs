using System.IdentityModel.Tokens.Jwt;
using blog_api.Data;
using blog_api.Models;
using Microsoft.EntityFrameworkCore;
using blog_api.Exceptions;

namespace blog_api.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TokenService _tokenService;
        private readonly BlacklistService _blacklistService;

        public UserService(ApplicationDbContext dbContext, TokenService tokenService, BlacklistService blacklistService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _blacklistService = blacklistService;
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
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Posts = new List<Guid>(),
                Likes = new List<Guid>()
            };
            
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            
            var token = _tokenService.GenerateJwtToken(user.Id);
            
            return new TokenResponse { Token = token };
        }

        public async Task<TokenResponse> LoginUser(LoginCredentials credentials)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == credentials.Email);
            if (user == null)
                throw new LoginException();

            if (!BCrypt.Net.BCrypt.Verify(credentials.Password, user.Password))
                throw new LoginException();

            var token = _tokenService.GenerateJwtToken(user.Id);

            return new TokenResponse { Token = token };
        }
        
        public async Task<Response> LogoutUser(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var expirationDate = jwtToken.ValidTo;

            await _blacklistService.AddToken(token, expirationDate);

            return new Response { Status = "200 OK", Message = "Logged out" };
        }
        
        public async Task<UserDto> GetUserProfile(Guid userId)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == userId);
            
            if (user == null)
                throw new UserException();
            
            var userDto = new UserDto
            {
                Id = user.Id,
                CreateTime = user.CreateTime,
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return userDto;
        }
        
        public async Task UpdateUserProfile(UserEditModel model, Guid userId)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new UserException();
            
            if (await _dbContext.Users.AnyAsync(u => u.Email == model.Email && u.Id != userId))
                throw new EmailException(model.Email);
            
            if (!string.IsNullOrEmpty(model.PhoneNumber) &&
                await _dbContext.Users.AnyAsync(u => u.PhoneNumber == model.PhoneNumber && u.Id != userId))
                throw new PhoneException(model.PhoneNumber);
            
            user.FullName = model.FullName;
            user.BirthDate = model.BirthDate;
            user.Gender = model.Gender;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}