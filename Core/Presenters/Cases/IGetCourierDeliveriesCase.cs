using Core.Infra.Repositories;
using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IGetCourierDeliveriesCase
    {
        public IEnumerable<GetSavedDeliveriesResponse> Execute(int UserId);
    }
}
