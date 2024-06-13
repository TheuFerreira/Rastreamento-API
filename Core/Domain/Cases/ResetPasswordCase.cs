using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using System.Net.Mail;
using System.Net;

namespace Core.Domain.Cases
{
    public class ResetPasswordCase : IResetPasswordCase
    {
        private readonly IUserRepository userRepository;
        private readonly Random random;

        public ResetPasswordCase(IUserRepository userRepository, Random random)
        {
            this.userRepository = userRepository;
            this.random = random;
        }

        public void Execute(string email)
        {
            UserModel user = userRepository.GetByEmail(email) ?? throw new NotFoundException();
            string newPassword = random.Next(100000, 999999).ToString();
            
            userRepository.UpdatePassword(user.UserId, newPassword);
            Console.WriteLine(newPassword);

            string body = $@"Hello.

Your new password is {newPassword}

-- Tracky
";

            string otherEmail = "tracky@beta.filmotopia.com.br";
            string otherPassword = "iTf\"$/9FS,*lAus";
            //
            MailMessage mensagemEmail = new(otherEmail, email, "Tracky - Nova Senha", body);

            SmtpClient client = new("smtp.titan.email", 587);
            NetworkCredential cred = new(otherEmail, otherPassword);
            client.EnableSsl = false;
            client.Credentials = cred;
            client.UseDefaultCredentials = true;

            client.Send(mensagemEmail);
        }
    }
}
