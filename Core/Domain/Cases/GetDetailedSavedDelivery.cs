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
        private readonly IAddressRepository addressRepository;

        public GetDetailedSavedDelivery(IDeliveryRepository deliveryRepository, IAddressRepository addressRepository)
        {
            this.deliveryRepository = deliveryRepository;
            this.addressRepository = addressRepository;
        }

        public DetailedDeliveryResponse Execute(int Deliveryid, int UserId)
        {
            DeliveryModel delivery = deliveryRepository.GetDeliveryByClientId(Deliveryid, UserId) ?? throw new NotFoundException();
            var addressDestiny = addressRepository.GetById(delivery.AddressDestinyId) ?? throw new NotFoundException();
            var addressOrigin = addressRepository.GetById(delivery.AddressOriginId) ?? throw new NotFoundException();

            var destiny = new DetailedDeliveryAddressResponse()
            {
                AddressId = addressDestiny.AddressId!.Value,
                CEP = addressDestiny.CEP,
                UF = addressDestiny.UF,
                City = addressDestiny.City,
                District = addressDestiny.District,
                Street = addressDestiny.Street,
                Number = addressDestiny.Number,
                Complement = addressDestiny.Complement,
            };

            var origin = new DetailedDeliveryAddressResponse()
            {
                AddressId = addressOrigin.AddressId!.Value,
                CEP = addressOrigin.CEP,
                UF = addressOrigin.UF,
                City = addressOrigin.City,
                District = addressOrigin.District,
                Street = addressOrigin.Street,
                Number = addressOrigin.Number,
                Complement = addressOrigin.Complement,
            };

            return new DetailedDeliveryResponse()
            {
                Destiny = destiny,
                Origin = origin,
                CurrentStatus = delivery.Status,
                LastUpdate = delivery.LastUpdateTime,
                CreatedDate = delivery.CreatedAt
            };

        }
    }
}
