using Core.Infra.Models;

namespace Core.Domain.Repositories
{
    public interface IDeliveryRepository
    {
        void Add(DeliveryModel delivery);  
        DeliveryModel? GetById(int Id);
    }
}
