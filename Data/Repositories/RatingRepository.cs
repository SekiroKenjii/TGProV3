using Core.Repositories;
using Domain.Entities;

namespace Data.Repositories
{
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(DataContext context) : base(context)
        {
        }
    }
}
