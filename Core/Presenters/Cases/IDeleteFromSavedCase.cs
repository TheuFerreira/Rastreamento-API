namespace Core.Presenters.Cases
{
    public interface IDeleteFromSavedCase
    {
        public void Execute(int deliveryId, int userId);
    }
}
