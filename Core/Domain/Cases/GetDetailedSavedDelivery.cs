using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class GetDetailedSavedDelivery : IGetDetailedSavedDeliveryCase
    {

        private readonly IDeliveryRepository deliveryRepository;

        public GetDetailedSavedDelivery(IDeliveryRepository deliveryRepository)
        {
            this.deliveryRepository = deliveryRepository;
        }

        public DetailedDeliveryResponse Execute(int Deliveryid, int UserId)
        {
            DeliveryModel delivery = deliveryRepository.GetDeliveryByClientId(Deliveryid, UserId) ?? throw new NotFoundException();
            return new DetailedDeliveryResponse()
            {
                Destiny = delivery.Destination,
                Origin = delivery.Origin,
                CurrentStatus = delivery.Status,
                LastUpdate = delivery.LastUpdateTime,
                CreatedDate = delivery.CreatedAt
            };

        }
    }
}
