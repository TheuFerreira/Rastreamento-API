using Core.Infra.Models;

namespace Core.Domain.Services
{
    public interface IEmailService
    {
        void Send(EmailBodyModel emailBody);
    }
}
