using Core;
using Core.Repositories;
using Data.Repositories;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DataContext _context;

        private IBrandRepository? _brands;
        private ICategoryRepository? _categories;
        private ICommentRepository? _comments;
        private IConditionRepository? _conditions;
        private IOrderRepository? _orders;
        private IOrderDetailRepository? _orderDetails;
        private IProductRepository? _products;
        private IProductPhotoRepository? _productPhotos;
        private IProductTypeRepository? _productTypes;
        private IRatingRepository? _ratings;
        private IRoleRepository? _roles;
        private IShoppingCartRepository? _shoppingCarts;
        private ISubBrandRepository? _subBrands;
        private IUserAddressRepository? _userAddresses;
        private IUserLoginTokenRepository? _userLoginTokens;
        private IUserRepository? _users;
        private IUserRoleRepository? _userRoles;
        private IWishListRepository? _wishLists;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IBrandRepository Brands => _brands ??= new BrandRepository(_context);

        public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);

        public ICommentRepository Comments => _comments ??= new CommentRepository(_context);

        public IConditionRepository Conditions => _conditions ??= new ConditionRepository(_context);

        public IOrderRepository Orders => _orders ??= new OrderRepository(_context);

        public IOrderDetailRepository OrderDetails => _orderDetails ??= new OrderDetailRepository(_context);

        public IProductRepository Products => _products ??= new ProductRepository(_context);

        public IProductPhotoRepository ProductPhotos => _productPhotos ??= new ProductPhotoRepository(_context);

        public IProductTypeRepository ProductTypes => _productTypes ??= new ProductTypeRepository(_context);

        public IRatingRepository Ratings => _ratings ??= new RatingRepository(_context);

        public IRoleRepository Roles => _roles ??= new RoleRepository(_context);

        public IShoppingCartRepository ShoppingCarts => _shoppingCarts ??= new ShoppingCartRepository(_context);

        public ISubBrandRepository SubBrands => _subBrands ??= new SubBrandRepository(_context);

        public IUserAddressRepository UserAddresses => _userAddresses ??= new UserAddressRepository(_context);

        public IUserLoginTokenRepository UserLoginTokens => _userLoginTokens ??= new UserLoginTokenRepository(_context);

        public IUserRepository Users => _users ??= new UserRepository(_context);

        public IUserRoleRepository UserRoles => _userRoles ??= new UserRoleRepository(_context);

        public IWishListRepository WishLists => _wishLists ??= new WishListRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
