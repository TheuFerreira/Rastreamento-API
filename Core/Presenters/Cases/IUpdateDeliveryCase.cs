using Core.Presenters.Requests;

namespace Core.Presenters.Cases
{
    public interface IUpdateDeliveryCase
    {
        void Execute(UpdateDeliveryRequest request, int userId);
    }
}
