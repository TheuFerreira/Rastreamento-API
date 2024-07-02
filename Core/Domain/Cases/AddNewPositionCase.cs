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
        private readonly IAddressRepository addressRepository;

        public AddNewPositionCase(IPositionDeliveryRepository positionDeliveryRepository, IDeliveryRepository deliveryRepository, IAddressRepository addressRepository)
        {
            this.positionDeliveryRepository = positionDeliveryRepository;
            this.deliveryRepository = deliveryRepository;
            this.addressRepository = addressRepository;
        }

        public void Execute(IList<NewPositionRequest> newPositionRequests)
        {
            foreach (NewPositionRequest newPositionRequest in newPositionRequests)
            {
                _ = deliveryRepository.GetById(newPositionRequest.DeliveryId) ?? throw new NotFoundException();

                int? addressId = null;
                NewPositionAddressRequest? addressResponse = newPositionRequest.Address;
                if (addressResponse != null)
                {
                    AddressModel address = new()
                    {
                        CEP = addressResponse.CEP,
                        City = addressResponse.City,
                        UF = addressResponse.UF,
                    };
                    addressId = addressRepository.Add(address);
                }

                PositionDeliveryModel model = new()
                {
                    AddressId = addressId,
                    CreatedAt = DateTime.UtcNow,
                    DeliveryId = newPositionRequest.DeliveryId,
                    Latitude = newPositionRequest.Latitude,
                    Longitude = newPositionRequest.Longitude,
                };
                positionDeliveryRepository.Insert(model);

                deliveryRepository.UpdateLastUpdateTime(newPositionRequest.DeliveryId, DateTime.UtcNow);
            }
        }
    }
}
