using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Requests;

namespace Core.Domain.Cases
{
    public class AddNewPositionCase : IAddNewPositionCase
    {
        private readonly IPositionDeliveryRepository positionDeliveryRepository;
        private readonly IDeliveryRepository deliveryRepository;

        public AddNewPositionCase(IPositionDeliveryRepository positionDeliveryRepository, IDeliveryRepository deliveryRepository)
        {
            this.positionDeliveryRepository = positionDeliveryRepository;
            this.deliveryRepository = deliveryRepository;
        }

        public void Execute(NewPositionRequest newPositionRequest)
        {
            _ = deliveryRepository.GetById(newPositionRequest.DeliveryId) ?? throw new NotFoundException();

            PositionDeliveryModel model = new()
            {
                CreatedAt = DateTime.UtcNow,
                DeliveryId = newPositionRequest.DeliveryId,
                Latitude = newPositionRequest.Latitude,
                Longitude = newPositionRequest.Longitude,
            };
            positionDeliveryRepository.Insert(model);
        }
    }
}
