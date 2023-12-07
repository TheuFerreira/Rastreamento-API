using Core.Presenters.Requests;
using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface ISearchDeliveryCase
    {
        IEnumerable<SearchResponse> Execute(SearchRequest request);
    }
}
