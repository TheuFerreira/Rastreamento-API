using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class GetCourierDeliveriesCase : IGetCourierDeliveriesCase
    {
        private readonly IDeliveryRepository deliveryRepository;
        public GetCourierDeliveriesCase(IDeliveryRepository deliveryRepository) 
        {
            this.deliveryRepository = deliveryRepository;
        }

        public CourierDeliveriesResponse Execute(int UserId)
        {
            IEnumerable<DeliveryModel> deliveries = deliveryRepository.GetDeliveriesByUserId(UserId);

            if (deliveries.Count() == 0 || deliveries == null) throw new NotFoundException();

            return new CourierDeliveriesResponse(deliveries);
            
        }
    }
}
