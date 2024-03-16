using Core.Infra.Models;

namespace Core.Domain.Repositories
{
    public interface IAddressRepository
    {
        int Add(AddressModel model);
    }
}
