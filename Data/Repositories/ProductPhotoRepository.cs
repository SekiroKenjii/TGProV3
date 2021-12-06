using Core.Repositories;
using Domain.Entities;

namespace Data.Repositories
{
    public class ProductPhotoRepository : RepositoryBase<ProductPhoto>, IProductPhotoRepository
    {
        public ProductPhotoRepository(DataContext context) : base(context)
        {
        }
    }
}
