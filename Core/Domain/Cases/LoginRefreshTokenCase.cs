using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Domain.Services;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;
using System.Security.Authentication;
using System.Text;

namespace Core.Domain.Cases
{
    public class LoginRefreshTokenCase : ILoginRefreshTokenCase
    {
        private readonly IJWTService jWTService;
        private readonly JWTModel jwtModel;
        private readonly IUserRepository userRepository;

        public LoginRefreshTokenCase(IJWTService jWTService, JWTModel jwtModel, IUserRepository userRepository)
        {
            this.jWTService = jWTService;
            this.jwtModel = jwtModel;
            this.userRepository = userRepository;
        }

        public SignInResponse Execute(LoginRefreshTokenRequest request)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(jwtModel.Expiration);
            byte[] key = Encoding.UTF8.GetBytes(jwtModel.Key);

            int userId;
            try
            {
                userId = jWTService.ValidateToken(request.RefreshToken, key);
            }
            catch (Exception)
            {
                throw new InvalidCredentialException();
            }

            UserModel model = userRepository.GetById(userId) ?? throw new NotFoundException();

            string token = jWTService.BuildToken(expiration, model.UserId, key);

            DateTime expirationRefreshToken = DateTime.UtcNow.AddHours(jwtModel.Expiration);
            string refreshToken = jWTService.BuildToken(expirationRefreshToken, model.UserId, key);

            SignInResponse response = new()
            {
                Token = token,
                ExpiresAt = expiration,
                RefreshToken = refreshToken,
            };
            return response;
        }
    }
}
