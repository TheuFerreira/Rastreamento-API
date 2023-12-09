using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IGetSavedDeliveryCase
    {
        BasicDeliveryResponse Execute(int DeliveryId, int ClientId);
    }
}
