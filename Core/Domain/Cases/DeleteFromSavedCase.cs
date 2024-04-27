using Core.Domain.Repositories;
using Core.Presenters.Cases;

namespace Core.Domain.Cases
{
    public class DeleteFromSavedCase : IDeleteFromSavedCase
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeleteFromSavedCase(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public void Execute(int deliveryId, int userId)
        {
            _deliveryRepository.RemoveFromSaved(deliveryId, userId);
        }
    }
}
