namespace Core.Presenters.Cases
{
    public interface IResetPasswordCase
    {
        void Execute(string email);
    }
}
