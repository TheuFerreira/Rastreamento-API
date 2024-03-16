using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class GetNotSavedDeliveryCase : IGetNotSavedDeliveryCase
    {
        private readonly IDeliveryRepository repository;
        private readonly IAddressRepository addressRepository;

        public GetNotSavedDeliveryCase(IDeliveryRepository repository, IAddressRepository addressRepository)
        {
            this.repository = repository;
            this.addressRepository = addressRepository;
        }

        public BasicDeliveryResponse Execute(int Id)
        {
            var delivery = repository.GetById(Id) ?? throw new NotFoundException();
            var address = addressRepository.GetById(delivery.AddressDestinyId) ?? throw new NotFoundException();

            var destiny = new BasicDeliveryAddressResponse()
            {
                AddressId = address.AddressId!.Value,
                CEP = address.CEP,
                UF = address.UF,
                City = address.City,
                District = address.District,
                Street = address.Street,
                Number = address.Number,
                Complement = address.Complement,
            };

            return new BasicDeliveryResponse()
            {
                LastUpdate = delivery.LastUpdateTime,
                CreatedDate = delivery.CreatedAt,
                Destiny = destiny
            };
        }
    }
}
