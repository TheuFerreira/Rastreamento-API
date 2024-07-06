using Core.Domain.Services;
using Core.Infra.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace Core.Infra.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettingsModel emailSettingsModel;

        public EmailService(EmailSettingsModel emailSettingsModel)
        {
            this.emailSettingsModel = emailSettingsModel;
        }

        public void Send(EmailBodyModel emailBody)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Support Trakky", emailSettingsModel.Email));
            emailMessage.To.Add(new MailboxAddress("Receiver Name", emailBody.Destiny));

            emailMessage.Subject = emailBody.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailBody.Body };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(emailSettingsModel.Host, emailSettingsModel.Port, false);

                smtp.Authenticate(emailSettingsModel.Email, emailSettingsModel.Password);

                smtp.Send(emailMessage);
                smtp.Disconnect(true);
            }
        }
    }
}
