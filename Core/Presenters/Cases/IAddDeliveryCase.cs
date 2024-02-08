using Core.Infra.Models;
using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IAddDeliveryCase
    {
        AddDeliveryResponse Execute(DeliveryModel model);
    }
}
