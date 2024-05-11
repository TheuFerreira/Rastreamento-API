using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IGetDeliveryByIdCase
    {
        DeliveryInfoResponse Execute(int id);
    }
}
