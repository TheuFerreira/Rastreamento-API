using Core.Domain.Exceptions;
using Core.Domain.Repositories;
using Core.Presenters.Cases;

namespace Core.Domain.Cases
{
    public class UserSaveDeliveryCase : IUserSaveDeliveryCase
    {
        private readonly IDeliveryRepository deliveryRepository;

        public UserSaveDeliveryCase(IDeliveryRepository deliveryRepository)
        {
            this.deliveryRepository = deliveryRepository;
        }

        public void Execute(int userId, int deliveryId)
        {
            _ = deliveryRepository.GetById(deliveryId) ?? throw new NotFoundException();

            bool has = deliveryRepository.UserSavedDelivery(userId, deliveryId);
            if (has)
                return;

            deliveryRepository.AddUser(deliveryId, userId);
        }
    }
}
