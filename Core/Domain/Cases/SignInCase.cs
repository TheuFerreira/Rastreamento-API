using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Domain.Cases
{
    public class SignInCase : ISignInCase
    {
        private readonly IUserRepository userRepository;
        private readonly JWTModel jwtModel;

        public SignInCase(IUserRepository userRepository, JWTModel jwtModel)
        {
            this.userRepository = userRepository;
            this.jwtModel = jwtModel;
        }

        public SignInResponse Execute(SignInRequest request)
        {
            UserModel model = userRepository.GetByEmailAndPassword(request.Email, request.Password)
                ?? throw new NotFoundException();

            DateTime expiration = DateTime.UtcNow.AddMinutes(jwtModel.Expiration);
            byte[] key = Encoding.UTF8.GetBytes(jwtModel.Key);
            string token = BuildToken(expiration, model.UserId, key);

            SignInResponse response = new()
            {
                Token = token,
                ExpiresAt = expiration,
            };
            return response;
        }

        private static string BuildToken(DateTime expiresAt, int userId, byte[] key)
        {
            Claim[] claims = new[]
            {
                new Claim("UserId", userId.ToString()),
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
