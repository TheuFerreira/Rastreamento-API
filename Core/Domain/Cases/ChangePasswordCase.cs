using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class ChangePasswordCase : IChangePasswordCase
    {
        private readonly IUserRepository _userRepository;
        public ChangePasswordCase(IUserRepository userRepository) 
        {
            this._userRepository = userRepository;
        }

        public ChangePasswordResponse Execute(string currentPassword, string newPassword, int userId) 
        {

            UserModel? validUser = _userRepository.GetByIdAndPassword(userId, currentPassword);

            if(validUser == null) 
            {
                return new ChangePasswordResponse()
                {
                    message = "Senha incorreta!",
                    status = false
                };
            }


            if (validUser.UserId == userId)
            {
                _userRepository.SetNewPassword(userId, currentPassword, newPassword);
                return new ChangePasswordResponse()
                {
                    message = "Senha alterada com sucesso!",
                    status = true
                };

            } else
            {
                return new ChangePasswordResponse()
                {
                    message = "Senha incorreta!",
                    status = false
                };
            }

        }
    }
}
