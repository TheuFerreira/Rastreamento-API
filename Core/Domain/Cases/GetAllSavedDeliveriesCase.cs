using Core.Domain.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class GetAllSavedDeliveriesCase : IGetAllSavedDeliveriesCase
    {
        private readonly IDeliveryRepository deliveryRepository;

        public GetAllSavedDeliveriesCase(IDeliveryRepository deliveryRepository)
        {
            this.deliveryRepository = deliveryRepository;
        }

        public IEnumerable<GetAllSavedDeliveriesResponse> Execute(int userId)
        {
            IEnumerable<GetAllSavedDeliveriesResponse> responses = deliveryRepository
                .GetAllUserSaved(userId)
                .Select(x => new GetAllSavedDeliveriesResponse
                {
                    Code = x.Code,
                    DeliveryId = x.DeliveryId,
                    Description = x.Description
                });

            return responses;
        }
    }
}
