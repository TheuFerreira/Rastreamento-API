using Core.Presenters.Requests;
using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IAddDeliveryCase
    {
        AddDeliveryResponse Execute(AddDeliveryRequest request, int userId);
    }
}
