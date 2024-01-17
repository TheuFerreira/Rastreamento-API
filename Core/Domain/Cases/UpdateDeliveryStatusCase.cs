using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Requests;

namespace Core.Domain.Cases
{
    public class UpdateDeliveryStatusCase : IUpdateDeliveryStatusCase
    {
        private readonly IDeliveryRepository deliveryRepository;

        public UpdateDeliveryStatusCase(IDeliveryRepository deliveryRepository)
        {
            this.deliveryRepository = deliveryRepository;
        }

        public void Execute(UpdateDeliveryStatusRequest request)
        {
            _ = deliveryRepository.GetById(request.DeliveryId) ?? throw new NotFoundException();
            deliveryRepository.UpdateStatus(request.DeliveryId, request.Status, DateTime.UtcNow);
        }
    }
}
