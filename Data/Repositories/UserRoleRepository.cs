using Core.Repositories;
using Domain.Entities;

namespace Data.Repositories
{
    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(DataContext context) : base(context)
        {
        }
    }
}
