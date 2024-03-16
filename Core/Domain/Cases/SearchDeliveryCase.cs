using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class SearchDeliveryCase : ISearchDeliveryCase
    {
        private readonly IDeliveryRepository deliveryRepository;

        public SearchDeliveryCase(IDeliveryRepository deliveryRepository)
        {
            this.deliveryRepository = deliveryRepository;
        }

        public IEnumerable<SearchResponse> Execute(SearchRequest request)
        {
            IEnumerable<DeliveryModel> models = deliveryRepository.GetByCode(request.Text);
            IEnumerable<SearchResponse> responses = models.Select(x => new SearchResponse()
            {
                Code = x.Code,
                DeliveryId = x.DeliveryId,
                LastUpdateTime = x.LastUpdateTime,
                Description = x.Description,
            });
            return responses;
        }
    }
}
