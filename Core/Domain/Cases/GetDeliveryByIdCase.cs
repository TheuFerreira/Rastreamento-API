using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class GetDeliveryByIdCase : IGetDeliveryByIdCase
    {
        private readonly IDeliveryRepository deliveryRepository;
        private readonly IAddressRepository addressRepository;

        public GetDeliveryByIdCase(IDeliveryRepository deliveryRepository, IAddressRepository addressRepository)
        {
            this.deliveryRepository = deliveryRepository;
            this.addressRepository = addressRepository;
        }

        public DeliveryInfoResponse Execute(int id)
        {
            DeliveryModel model = deliveryRepository.GetById(id) ?? throw new NotFoundException();

            AddressModel addressDestiny = addressRepository.GetById(model.AddressDestinyId)!;
            AddressModel addressOrigin = addressRepository.GetById(model.AddressOriginId)!;

            DeliveryInfoResponse response = new()
            {
                DeliveryId = id,
                Observation = model.Observation,
                Description = model.Description,
                Destiny = new DeliveryAddressInfoResponse()
                {
                    AddressId = addressDestiny.AddressId,
                    CEP = addressDestiny.CEP,
                    UF = addressDestiny.UF,
                    City = addressDestiny.City,
                    District = addressDestiny.District,
                    Street = addressDestiny.Street,
                    Number = addressDestiny.Number,
                    Complement = addressDestiny.Complement,
                },
                Origin = new DeliveryAddressInfoResponse()
                {
                    AddressId = addressOrigin.AddressId,
                    CEP = addressOrigin.CEP,
                    UF = addressOrigin.UF,
                    City = addressOrigin.City,
                    District = addressOrigin.District,
                    Street = addressOrigin.Street,
                    Number = addressOrigin.Number,
                    Complement = addressOrigin.Complement,
                },
            };
            return response;
        }
    }
}
