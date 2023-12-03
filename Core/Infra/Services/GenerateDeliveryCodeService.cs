using Core.Domain.Services;

namespace Core.Infra.Services
{
    public class GenerateDeliveryCodeService : IGenerateDeliveryCodeService
    {
        public string GenerateDeliveryCode()
        {
            return Guid.NewGuid().ToString().ToString();
        }
    }
}
