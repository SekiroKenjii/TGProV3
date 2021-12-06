using Core.Repositories;
using Domain.Entities;

namespace Data.Repositories
{
    public class UserLoginTokenRepository : RepositoryBase<UserLoginToken>, IUserLoginTokenRepository
    {
        public UserLoginTokenRepository(DataContext context) : base(context)
        {
        }
    }
}
