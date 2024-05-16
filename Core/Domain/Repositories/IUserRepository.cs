using Core.Infra.Models;

namespace Core.Domain.Repositories
{
    public interface IUserRepository
    {
        UserModel? GetByEmailAndPassword(string email, string password);
        UserModel? GetByEmail(string email);
        void Add(UserModel userModel);
        UserModel? GetById(int userId);
        void Update(UserModel userModel);
        UserModel? GetByIdAndPassword(int userId, string password);
        void SetNewPassword(int userId, string currentPassword, string newPassword);
    }
}
