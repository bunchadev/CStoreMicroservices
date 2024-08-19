using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace User.API.Services
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        public string GenerateToken(Guid id, string role, int hours)
        {
            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier,id.ToString()),
              new Claim(ClaimTypes.Role,role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? ""));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt_Token = new JwtSecurityToken(
                 configuration["Jwt:Issuer"],
                 configuration["Jwt:Audience"],
                 claims,
                 expires: DateTime.Now.AddHours(hours),
                 signingCredentials: credentials);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwt_Token);
            return jwtToken;
        }
    }
}
