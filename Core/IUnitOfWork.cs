using Core.Repositories;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBrandRepository Brands { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        IConditionRepository Conditions { get; }
        IOrderRepository Orders { get; }
        IOrderDetailRepository OrderDetails { get; }
        IProductRepository Products { get; }
        IProductPhotoRepository ProductPhotos { get; }
        IProductTypeRepository ProductTypes { get; }
        IRatingRepository Ratings { get; }
        IRoleRepository Roles { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        ISubBrandRepository SubBrands { get; }
        IUserAddressRepository UserAddresses { get; }
        IUserLoginTokenRepository UserLoginTokens { get; }
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }
        IWishListRepository WishLists { get; }

        Task<int> SaveChangeAsync();
    }
}
