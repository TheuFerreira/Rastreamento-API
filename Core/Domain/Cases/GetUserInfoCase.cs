using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class GetUserInfoCase : IGetUserInfoCase
    {
        private readonly IUserRepository userRepository;

        public GetUserInfoCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserInfoResponse Execute(int userId)
        {
            UserModel model = userRepository.GetById(userId) ?? throw new NotFoundException();

            return new UserInfoResponse()
            {
                Email = model.Email,
                FullName = model.FullName,
                BirthDate = model.GetDateFromBirthDate(),
            };
        }
    }
}
