using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Responses;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Core.Domain.Cases
{
    public class GetCourierDeliveriesCase : IGetCourierDeliveriesCase
    {
        private readonly IDeliveryRepository deliveryRepository;
        public GetCourierDeliveriesCase(IDeliveryRepository deliveryRepository) 
        {
            this.deliveryRepository = deliveryRepository;
        }

        public IEnumerable<GetSavedDeliveriesResponse> Execute(int UserId)
        {
            IEnumerable<DeliveryModel> deliveries = deliveryRepository.GetDeliveriesByUserId(UserId);

            if (deliveries.Count() == 0 || deliveries == null) throw new NotFoundException();

            IEnumerable<GetSavedDeliveriesResponse> response = deliveries.Select(x => new GetSavedDeliveriesResponse()
            {
                DeliveryId=x.DeliveryId,
                CourierId=x.CourierId,
                Observation=x.Observation,
                Description=x.Description,
                AddressOriginId=x.AddressOriginId,
                AddressDestinyId=x.AddressDestinyId,
                Code=x.Code,
                Status=x.Status,
                LastUpdateTime=x.LastUpdateTime,
                CreatedAt=x.CreatedAt,
            });

            return response;
            
        }
    }
}
