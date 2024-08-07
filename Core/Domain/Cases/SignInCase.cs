﻿using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Domain.Services;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;
using System.Text;

namespace Core.Domain.Cases
{
    public class SignInCase : ISignInCase
    {
        private readonly IUserRepository userRepository;
        private readonly JWTModel jwtModel;
        private readonly IJWTService jWTService;

        public SignInCase(IUserRepository userRepository, JWTModel jwtModel, IJWTService jWTService)
        {
            this.userRepository = userRepository;
            this.jwtModel = jwtModel;
            this.jWTService = jWTService;
        }

        public SignInResponse Execute(SignInRequest request)
        {
            UserModel model = userRepository.GetByEmailAndPassword(request.Email, request.Password)
                ?? throw new NotFoundException();

            DateTime expiration = DateTime.UtcNow.AddMinutes(jwtModel.Expiration);
            byte[] key = Encoding.UTF8.GetBytes(jwtModel.Key);
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
