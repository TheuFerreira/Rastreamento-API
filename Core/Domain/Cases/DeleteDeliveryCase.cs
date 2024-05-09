using Core.Domain.Repositories;
using Core.Presenters.Cases;

namespace Core.Domain.Cases
{
    public class DeleteDeliveryCase : IDeleteDeliveryCase
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeleteDeliveryCase(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public void Execute(int id)
        {
            _deliveryRepository.Delete(id);
        }
    }
}
