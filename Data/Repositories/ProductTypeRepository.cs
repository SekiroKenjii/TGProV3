using Core.Repositories;
using Domain.Entities;

namespace Data.Repositories
{
    public class ProductTypeRepository : RepositoryBase<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(DataContext context) : base(context)
        {
        }
    }
}
