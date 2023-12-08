using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class GetNotSavedDelivery : IGetNotSavedDelivery
    {
        private readonly IDeliveryRepository repository;
        public GetNotSavedDelivery(IDeliveryRepository repository) 
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
