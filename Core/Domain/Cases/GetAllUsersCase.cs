using Core.Domain.Repositories;
using Core.Presenters.Cases;

namespace Core.Domain.Cases
{
    public class GetAllUsersCase : IGetAllUsersCase
    {
        private readonly IUserRepository userRepository;
        public GetAllUsersCase(IUserRepository userRepository)
        {
			this.userRepository = userRepository;
		}
    }
}