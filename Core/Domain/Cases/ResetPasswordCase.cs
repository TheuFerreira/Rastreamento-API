using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Domain.Services;
using Core.Infra.Models;
using Core.Presenters.Cases;

namespace Core.Domain.Cases
{
    public class ResetPasswordCase : IResetPasswordCase
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;
        private readonly Random random;

        public ResetPasswordCase(IUserRepository userRepository, IEmailService emailService, Random random)
        {
            this.userRepository = userRepository;
            this.emailService = emailService;
            this.random = random;
        }

        public void Execute(string email)
        {
            UserModel user = userRepository.GetByEmail(email) ?? throw new NotFoundException();
            string newPassword = random.Next(100000, 999999).ToString();

            userRepository.UpdatePassword(user.UserId, newPassword);

            string body = $@"Hello.

Your new password is <strong>{newPassword}</strong></br></br>

-- Tracky
";

            EmailBodyModel emailBody = new()
            {
                Destiny = email,
                Subject = "Trakky - Reset Password",
                Body = body,
            };
            emailService.Send(emailBody);
        }
    }
}
