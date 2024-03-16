using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Domain.Services;
using Core.Domain.Utils;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Requests;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class AddDeliveryCase : IAddDeliveryCase
    {
        private readonly IDeliveryRepository deliveryRepository;
        private readonly IUserRepository userRepository;
        private readonly IGenerateDeliveryCodeService generateDeliveryCodeService;
        private readonly IAddressRepository addressRepository;

        public AddDeliveryCase(IDeliveryRepository deliveryRepository, IUserRepository userRepository, IGenerateDeliveryCodeService generateDeliveryCodeService, IAddressRepository addressRepository)
        {
            this.deliveryRepository = deliveryRepository;
            this.userRepository = userRepository;
            this.generateDeliveryCodeService = generateDeliveryCodeService;
            this.addressRepository = addressRepository;
        }

        public AddDeliveryResponse Execute(AddDeliveryRequest request, int userId)
        {
            if (
                request.Destiny == null
                || request.Observation == null
                || request.Description == null
                || request.Origin == null)
                throw new BadHttpRequestException("Preencha todos os campos disponíveis!");

            _ = userRepository.GetById(userId) ?? throw new NotFoundException();

            AddressModel origin = new()
            {
                CEP = request.Origin.CEP,
                City = request.Origin.City,
                UF = request.Origin.UF,
                District = request.Origin.District,
                Street = request.Origin.Street,
                Number = request.Origin.Number,
                Complement = request.Origin.Complement,
            };

            AddressModel destiny = new()
            {
                CEP = request.Destiny.CEP,
                City = request.Destiny.City,
                UF = request.Destiny.UF,
                District = request.Destiny.District,
                Street = request.Destiny.Street,
                Number = request.Destiny.Number,
                Complement = request.Destiny.Complement,
            };

            origin.AddressId = addressRepository.Add(origin);
            destiny.AddressId = addressRepository.Add(destiny);

            DeliveryModel model = new(request.Observation, request.Description, origin.AddressId.Value, destiny.AddressId.Value, userId)
            {
                Status = (int)DeliveryStatus.WaitingCollect,
                Code = generateDeliveryCodeService.GenerateDeliveryCode(),
                CreatedAt = DateTime.UtcNow,
                LastUpdateTime = DateTime.UtcNow,
            };

            int id = this.deliveryRepository.Add(model);

            return new AddDeliveryResponse
            {
                Id = id,
                Code = model.Code
            };
        }
    }
}
