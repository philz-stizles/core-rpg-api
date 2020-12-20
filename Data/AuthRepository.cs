using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using core_rpg_mvc.Models;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace core_rpg_mvc.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        bool Exists(string username);
    }

    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _ctx;
        private readonly IConfiguration _config;
        public AuthRepository(DataContext ctx, IMapper mapper, IConfiguration config)
        {
            _config = config;
            _ctx = ctx;
        }

        public bool Exists(string username)
        {
            return _ctx.Users.Any(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var existingUser = await _ctx.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
            if (existingUser == null) throw new System.Exception("Invalid credentials");

            var passwordValid = Utility.VerifyPasswordHash(password, existingUser.PasswordHash, existingUser.PasswordSalt);
            if (!passwordValid) throw new System.Exception("Invalid credentials");
            return new ServiceResponse<string> { Status = true, Data = CreateToken(existingUser), Message = "Login successfully" };
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (Exists(user.Username)) throw new System.Exception("User already exists");

            Utility.CreatePasswordHash(password, out byte[] passwordahash, out byte[] passwordSalt);

            user.PasswordHash = passwordahash;
            user.PasswordSalt = passwordSalt;

            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();

            return new ServiceResponse<int> { Status = true, Data = user.Id, Message = "Created successfully" };
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:JWTToken").Value));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddSeconds(3600),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}