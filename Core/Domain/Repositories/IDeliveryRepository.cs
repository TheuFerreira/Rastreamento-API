using Core.Infra.Models;

namespace Core.Domain.Repositories
{
    public interface IDeliveryRepository
    {
        void Add(DeliveryModel delivery);
        IEnumerable<DeliveryModel> GetByCode(string code);
        DeliveryModel? GetById(int Id);
        DeliveryModel? GetDeliveryByClientId(int DeliveryId, int ClientId);
        void UpdateStatus(int deliveryId, int status, DateTime currentTime);
    }
}
