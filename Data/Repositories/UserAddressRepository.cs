using Core.Repositories;
using Domain.Entities;

namespace Data.Repositories
{
    public class UserAddressRepository : RepositoryBase<UserAddress>, IUserAddressRepository
    {
        public UserAddressRepository(DataContext context) : base(context)
        {
        }
    }
}
