using BAL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<string> Login(User user)
        {
            User login =await _userRepository.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (login == null)
                return "Wrong username or password";
            string jwt = await GenerateToken(login);
            return jwt;
        }

        public async Task<string> Register(User user)
        {
            User register = await _userRepository.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (register != null)
                return "Duplicated user";
            _userRepository.Add(user);
            return "User added successfully";

        }
        private async Task<string> GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                audience: _configuration["Jwt:Audience"],
                issuer: _configuration["Jwt:Issuer"],
                signingCredentials: credential,
                //client-side set cookies for 30 days
                expires: DateTime.UtcNow.AddDays(30)
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
