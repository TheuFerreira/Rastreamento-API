using Core.Infra.Models;

namespace Core.Domain.Repositories
{
    public interface IDeliveryRepository
    {
        int Add(DeliveryModel delivery);
        IEnumerable<DeliveryModel> GetByCode(string code);
        IEnumerable<DeliveryModel> GetDeliveriesByUserId(int UserId);
        DeliveryModel? GetById(int Id);
        DeliveryModel? GetDeliveryByClientId(int DeliveryId, int ClientId);
        void UpdateStatus(int deliveryId, int status, DateTime currentTime);
        void UpdateLastUpdateTime(int deliveryId, DateTime currentTime);
    }
}
