using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class GetDeliveryDetailsByIdCase : IGetDeliveryDetailsByIdCase
    {
        private readonly IDeliveryRepository deliveryRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IPositionDeliveryRepository positionDeliveryRepository;

        public GetDeliveryDetailsByIdCase(IDeliveryRepository deliveryRepository, IAddressRepository addressRepository, IPositionDeliveryRepository positionDeliveryRepository)
        {
            this.deliveryRepository = deliveryRepository;
            this.addressRepository = addressRepository;
            this.positionDeliveryRepository = positionDeliveryRepository;
        }

        public GetDeliveryDetailsByIdResponse Execute(int id)
        {
            DeliveryModel delivery = deliveryRepository.GetById(id) ?? throw new NotFoundException();
            AddressModel address = addressRepository.GetById(delivery.AddressOriginId) ?? throw new NotFoundException();
            PositionDeliveryModel? position = positionDeliveryRepository.GetMostRecentByDelivery(id);

            GetPositionDeliveryResponse? positionResponse = null;
            if (position != null)
            {
                int? addressId = position.AddressId;
                GetCEPDeliveryResponse? cepPositionResponse = null;
                if (addressId.HasValue)
                {
                    AddressModel? addressModel = addressRepository.GetById(addressId.Value);
                    if (addressModel != null)
                    {
                        cepPositionResponse = new()
                        {
                            CEP = addressModel.CEP,
                            City = addressModel.City,
                            UF = addressModel.UF
                        };
                    }
                }

                positionResponse = new()
                {
                    Latitude = position.Latitude,
                    Longitude = position.Longitude,
                    Address = cepPositionResponse,
                };
            }

            GetCEPDeliveryResponse cepResponse = new()
            {
                CEP = address.CEP,
                City = address.City,
                UF = address.UF,
            };

            GetDeliveryDetailsByIdResponse response = new()
            {
                Description = delivery.Description,
                CreatedAt = delivery.CreatedAt,
                LastUpdateTime = delivery.LastUpdateTime,
                Address = cepResponse,
                LastPosition = positionResponse,
            };

            return response;
        }
    }
}
