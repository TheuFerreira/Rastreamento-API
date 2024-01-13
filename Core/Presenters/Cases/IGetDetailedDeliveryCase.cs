using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IGetDetailedSavedDeliveryCase
    {
        DetailedDeliveryResponse Execute(int Deliveryid, int UserId);
    }
}
