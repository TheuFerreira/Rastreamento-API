using Core.Presenters.Responses;

namespace Core.Presenters.Cases
{
    public interface IChangePasswordCase
    {
        ChangePasswordResponse Execute(string currentPassword, string newPassword, int userId);
    }
}
