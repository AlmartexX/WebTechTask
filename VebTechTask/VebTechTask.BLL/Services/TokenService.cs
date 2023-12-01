using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VebTechTask.BLL.Services
{
    public class TokenService
    {
        public string GenerateUserToken(string email,string secretKey)
        {
            return GenerateToken(email, "User", 60, secretKey);
        }

        public string GenerateAdminToken(string email, string secretKey)
        {
            return GenerateToken(email, "Admin", 60, secretKey);
        }

        private string GenerateToken(string email, string role, int expiryMinutes, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            }),
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer = "WebTech",
                Audience = "https://localhost:5001",
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
