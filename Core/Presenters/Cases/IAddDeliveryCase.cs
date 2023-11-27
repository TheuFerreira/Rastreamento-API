using Core.Infra.Models;

namespace Core.Presenters.Cases
{
    public interface IAddDeliveryCase
    {
        void Execute(DeliveryModel model);
    }
}
