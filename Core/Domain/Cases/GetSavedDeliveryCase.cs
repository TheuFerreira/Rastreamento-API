using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class GetSavedDeliveryCase : IGetSavedDeliveryCase
    {
        private readonly IDeliveryRepository repository;
        public GetSavedDeliveryCase(IDeliveryRepository repository) 
        {
            this.repository = repository;
        }

        public BasicDeliveryResponse Execute(int DeliveryId, int ClientId)
        {

            var delivery = repository.GetDeliveryByClientId(DeliveryId, ClientId) ?? throw new NotFoundException();

            return new BasicDeliveryResponse()
            {
                LastUpdate = delivery.LastUpdateTime,
                CreatedDate = delivery.CreatedAt,
                Destiny = delivery.Destination
            };
        }
    }
}
