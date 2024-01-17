using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class GetNotSavedDeliveryCase : IGetNotSavedDeliveryCase
    {
        private readonly IDeliveryRepository repository;
        public GetNotSavedDeliveryCase(IDeliveryRepository repository)
        {
            this.repository = repository;
        }

        public BasicDeliveryResponse Execute(int Id)
        {

            var delivery = repository.GetById(Id) ?? throw new NotFoundException();

            return new BasicDeliveryResponse()
            {
                LastUpdate = delivery.LastUpdateTime,
                CreatedDate = delivery.CreatedAt,
                Destiny = delivery.Destination
            };
        }
    }
}
