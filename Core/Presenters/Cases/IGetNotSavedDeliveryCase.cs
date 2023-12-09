using Core.Presenters.Requests;
using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IGetNotSavedDeliveryCase
    {
        BasicDeliveryResponse Execute(int id);
    }
}
