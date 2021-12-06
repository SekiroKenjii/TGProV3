using Core.Repositories;
using Domain.Entities;

namespace Data.Repositories
{
    public class WishListRepository : RepositoryBase<WishList>, IWishListRepository
    {
        public WishListRepository(DataContext context) : base(context)
        {
        }
    }
}
