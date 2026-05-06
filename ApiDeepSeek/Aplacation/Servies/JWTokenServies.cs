using ApiDeepSeek.Aplacation.InterfaceServies;
using ApiDeepSeek.Aplacation.Servies;
using ApiDeepSeek.Doamin.Interfais;
using ApiDeepSeek.Infrastructure.Repotisory;
using ApiDeepSeek.models;
using GroupApi.Controllers;
using GroupApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ApiDeepSeek.Aplacation.Servies
{
    public class JWTokenServies : IJWTokenServiescs
    {
        private readonly IConfiguration _configuration;
        private IUserRepotisory _userRepotisory;

        public JWTokenServies(IConfiguration configuration,IUserRepotisory userRepotisory)
        {
            _configuration = configuration;
            _userRepotisory = userRepotisory;
        }

        // Потом в методе:
     
        public async Task<string> SignAnonimal(User user)
        {
            var existingUser = await _userRepotisory.GetUserId(user.UserName);

            if (existingUser.Data == null)
            {
                // первый раз - генерируем новый id и сохраняем
                user.UserId = Guid.NewGuid().ToString();
                await _userRepotisory.AddUser(user);

            }
            else
            {
                // уже есть - берём его id
                user.UserId = existingUser.Data.UserId;
            }
            var jwtKey = _configuration["Jwt:Key"];
      
            var claims = new[] {
    new Claim("userId", user.UserId),
    new Claim("username", user.UserName)
};
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
    claims: claims,
    expires: DateTime.UtcNow.AddDays(7),
    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
); return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        }
    }
}
