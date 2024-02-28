using Core.Infra.Models;

namespace Core.Domain.Repositories
{
    public interface IPositionDeliveryRepository
    {
        void Insert(PositionDeliveryModel positionDelivery);
    }
}
