using Core.Domain.Entities;

namespace Core.Domain.Repositories { 

	public interface IUserRepository
	{
		User Get(int id);
		void Create(User user);
		void Update(User user);
		bool Delete(int id);
		IEnumerable<User> GetAll();
	}
}
