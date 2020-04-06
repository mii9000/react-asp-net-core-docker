using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SPA.Web;

public interface IJwtService
    {
        string GetToken(string userAccountId, string issuer, string audience);
    }

    public class JwtService : IJwtService
    {
        private readonly AuthSecrets _jwtConfig;

        public JwtService(IOptions<AuthSecrets> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        public string GetToken(string userAccountId, string issuer, string audience)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.ClientSecret));
            var mySigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userAccountId)
            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: mySigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
