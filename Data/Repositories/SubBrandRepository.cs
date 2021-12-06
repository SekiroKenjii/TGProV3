using Core.Repositories;
using Domain.Entities;

namespace Data.Repositories
{
    public class SubBrandRepository : RepositoryBase<SubBrand>, ISubBrandRepository
    {
        public SubBrandRepository(DataContext context) : base(context)
        {
        }
    }
}
