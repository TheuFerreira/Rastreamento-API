using Core.Domain.Repositories;
using Core.Infra.Models;
using Core.Presenters.Cases;
using Core.Presenters.Responses;

namespace Core.Domain.Cases
{
    public class GetUserDeliveriesCase : IGetUserDeliveriesCase
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IAddressRepository _addressRepository;

        public GetUserDeliveriesCase(IDeliveryRepository deliveryRepository, IAddressRepository addressRepository)
        {
            _deliveryRepository = deliveryRepository;
            _addressRepository = addressRepository;
        }

        public IEnumerable<GetUserDeliveriesResponse> Execute(int userId)
        {
            IEnumerable<DeliveryModel> models = _deliveryRepository.GetAllOfUser(userId);

            IEnumerable<GetUserDeliveriesResponse> responses = models.Select(x =>
            {
                AddressModel destinyModel = _addressRepository.GetById(x.AddressDestinyId)!;
                AddressModel originModel = _addressRepository.GetById(x.AddressOriginId)!;

                GetUserDeliveriesAddressResponse destiny = new()
                {
                    City = destinyModel.City,
                    UF = destinyModel.UF,
                };

                GetUserDeliveriesAddressResponse origin = new()
                {
                    City = originModel.City,
                    UF = originModel.UF,
                };

                string name = x.Description ?? x.Code;

                return new GetUserDeliveriesResponse
                {
                    DeliveryId = x.DeliveryId,
                    Destiny = destiny,
                    LastUpdateTime = x.LastUpdateTime,
                    Name = name,
                    Origin = origin,
                    Status = x.Status
                };
            });

            return responses;
        }
    }
}
