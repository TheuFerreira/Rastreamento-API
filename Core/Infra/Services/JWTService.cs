using Core.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Core.Infra.Services
{
    public class JWTService : IJWTService
    {
        public string BuildToken(DateTime expiresAt, int userId, byte[] key)
        {
            Claim[] claims = new[]
            {
                new Claim("UserId", userId.ToString()),
            };

            SymmetricSecurityKey authSigningKey = new(key);

            JwtSecurityToken jwtSecurityToken = new(
                expires: expiresAt,
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }

        public int ValidateToken(string token, byte[] key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(key);

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);

            return userId;
        }

        private static TokenValidationParameters GetValidationParameters(byte[] key)
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                //ValidIssuer = "Sample",
                //ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(key) // The same key as the one that generate the token
            };
        }
    }
}
