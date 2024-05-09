using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Requests;

namespace Core.Domain.Cases
{
    public class UpdateDeliveryCase : IUpdateDeliveryCase
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IAddressRepository _addressRepository;

        public UpdateDeliveryCase(IDeliveryRepository deliveryRepository, IAddressRepository addressRepository)
        {
            _deliveryRepository = deliveryRepository;
            _addressRepository = addressRepository;
        }

        public void Execute(UpdateDeliveryRequest request, int userId)
        {
            DeliveryModel model = _deliveryRepository.GetById(request.DeliveryId) ?? throw new NotFoundException();

            AddressModel origin = new()
            {
                AddressId = model.AddressOriginId,
                CEP = request.Origin.CEP,
                City = request.Origin.City,
                Complement = request.Origin.Complement,
                District = request.Origin.District,
                Number = request.Origin.Number,
                Street = request.Origin.Street,
                UF = request.Origin.UF,
            };

            AddressModel destiny = new()
            {
                AddressId = model.AddressDestinyId,
                CEP = request.Destiny.CEP,
                City = request.Destiny.City,
                Complement = request.Destiny.Complement,
                District = request.Destiny.District,
                Number = request.Destiny.Number,
                Street = request.Destiny.Street,
                UF = request.Destiny.UF,
            };

            _addressRepository.Update(origin);
            _addressRepository.Update(destiny);

            model.Description = request.Description;
            model.Observation = request.Observation;

            _deliveryRepository.Update(model);
        }
    }
}
