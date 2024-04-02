using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IGetAllSavedDeliveriesCase
    {
        IEnumerable<GetAllSavedDeliveriesResponse> Execute(int userId);
    }
}
