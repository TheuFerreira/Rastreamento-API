using Core.Domain.Entities;
using Core.Domain.Repositories;

namespace Core.Infra.Repositories
{
    public class UserRepositoryNoDb : IUserRepository
    {
        public UserRepositoryNoDb() { }

        public void Create(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }

}