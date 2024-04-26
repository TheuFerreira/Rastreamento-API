using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IGetUserDeliveriesCase
    {
        public IEnumerable<GetUserDeliveriesResponse> Execute(int userId);
    }
}
