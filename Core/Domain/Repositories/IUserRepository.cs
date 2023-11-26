using Core.Infra.Models;

namespace Core.Domain.Repositories
{
    public interface IUserRepository
    {
        UserModel? GetByEmailAndPassword(string email, string password);
    }
}
