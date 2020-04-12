using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SPA.Web;
using SPA.Web.Models;

public interface IJwtService
    {
        string GetToken(int userId, string email);
    }

    public class JwtService : IJwtService
    {
        private readonly AuthSecretsConfig _jwtConfig;

        public JwtService(IOptions<AuthSecretsConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        public string GetToken(int userId, string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.ClientSecret));
            var mySigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email)
            };

            var token = new JwtSecurityToken(
                _jwtConfig.Issuer,
                _jwtConfig.ClientId,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: mySigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
