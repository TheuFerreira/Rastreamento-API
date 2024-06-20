using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class EditUserCase : IEditUserCase
    {
        private readonly IUserRepository userRepository;

        public EditUserCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public SignUpResponse Execute(EditUserRequest request, int userId)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly maxBirthDate = currentDate.AddYears(-120);
            
            if (string.IsNullOrEmpty(request.Email)) throw new BadRequestException("Credenciais inválidas!");
            if (string.IsNullOrEmpty(request.Fullname)) throw new BadRequestException("Informe o nome do usuário!");
            if (request.BirthDate > currentDate || request.BirthDate < maxBirthDate) throw new BadRequestException("Informe uma data de nascimento válida!");

            UserModel oldUser = userRepository.GetById(userId)!;
            if (oldUser.Email != request.Email)
                _ = userRepository.GetByEmail(request.Email) ?? throw new BadRequestException("E-mail já cadastrado!");

            UserModel user = new()
            {
                FullName = request.Fullname,
                Email = request.Email,
                Password = request.Password,
                BirthDate = request.BirthDate.ToDateTime(TimeOnly.MinValue),
                UserId = userId
            };

            userRepository.Update(user);

            SignUpResponse response = new()
            {
                Success = true,
                Message = "Usuário editado com sucesso!"
            };

            return response;
        }
    }
}
