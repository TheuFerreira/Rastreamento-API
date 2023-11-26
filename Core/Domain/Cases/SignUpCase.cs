using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class SignUpCase : ISignUpCase
    {
        private readonly IUserRepository userRepository;

        public SignUpCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public SignUpResponse Execute(SignUpRequest request)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            var maxBirthDate = currentDate.AddYears(-120);

            if (request == null) throw new BadRequestException("Ocorreu um erro ao tentar cadastrar o usuário! Verifique se todos os dados foram preenchidos corretamente");
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password)) throw new BadRequestException("Credenciais inválidas!");
            if (string.IsNullOrEmpty(request.Fullname)) throw new BadRequestException("Informe o nome do usuário!");
            if (request.BirthDate > currentDate || request.BirthDate < maxBirthDate) throw new BadRequestException("Informe uma data de nascimento válida!");

            var userAlreadyExists = userRepository.GetByEmail(request.Email);

            if (userAlreadyExists != null) throw new BadRequestException("e-mail já cadastrado!");

            userRepository.Add(new Infra.Models.UserModel(request.Fullname, request.Email, request.Password, request.BirthDate.ToDateTime(TimeOnly.MinValue)));

            SignUpResponse response = new()
            {
                Success = true,
                Message = "Usuário cadastrado com sucesso!"
            };

            return response;


        }
    }
}
