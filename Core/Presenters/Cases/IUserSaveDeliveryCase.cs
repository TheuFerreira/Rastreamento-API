namespace Core.Presenters.Cases
{
    public interface IUserSaveDeliveryCase
    {
        public void Execute(int userId, int deliveryId);
    }
}
