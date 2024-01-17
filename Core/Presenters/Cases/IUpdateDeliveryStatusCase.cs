using Core.Presenters.Requests;

namespace Core.Presenters.Cases
{
    public interface IUpdateDeliveryStatusCase
    {
        void Execute(UpdateDeliveryStatusRequest request);
    }
}
