using Core.Presenters.Requests;
using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IGetNotSavedDelivery
    {
        BasicDeliveryResponse Execute(int id);
    }
}
