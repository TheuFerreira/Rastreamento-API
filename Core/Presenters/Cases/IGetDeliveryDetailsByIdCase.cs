using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IGetDeliveryDetailsByIdCase
    {
        GetDeliveryDetailsByIdResponse Execute(int id);
    }
}
