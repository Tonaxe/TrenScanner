using DavxeShop.Library.Services.Interfaces;
using DavxeShop.Persistance.Interfaces;
using DavxeShop.Persistance;
using Microsoft.EntityFrameworkCore;
using DavxeShop.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DavxeShop.Library.Services
{
    public class UserService : IUserService
    {
        private readonly ITrenDboHelper _trenDboHelper;
        private readonly IConfiguration _configuration;

        public UserService(IDbContextFactory<TrenScannerContext> contextFactory, ITrenDboHelper trenDboHelper, IConfiguration configuration)
        {
            _trenDboHelper = trenDboHelper;
            _configuration = configuration;
        }

        public bool RegisterUser(UserData userData)
        {
            return _trenDboHelper.AddUser(userData);
        }

        public UserDbData GetUser(string user)
        {
            return _trenDboHelper.GetUser(user);
        }

        public string GenerateJwtToken(string user)
        {
            var secretKey = _configuration["AppSettings:Secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user)
            };

            var token = new JwtSecurityToken(
                issuer: "TrenScanner",
                audience: "TrenScanner",
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void SaveToken(string user, string token)
        {
            var userInDb = _trenDboHelper.GetUserDb(user);
            if (userInDb != null)
            {
                userInDb.Token = token;
                _trenDboHelper.SaveChanges();
            }
        }
    }
}
