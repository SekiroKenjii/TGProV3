using AutoMapper;
using Core;
using Core.Accessors;
using Core.Constants;
using Core.DTOs.Product;
using Core.Exceptions;
using Core.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<ProductDto> AddProduct(AddProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            product.CreatedBy = _userAccessor.GetUserId();

            await _unitOfWork.Products.AddAsync(product);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            if (!isSaved) throw new Exception(Errors.ADD_FAILURE);

            var result = _mapper.Map<ProductDto>(product);

            result.CreatedInfo = await _userAccessor.GetCreatedInfo(product.CreatedAt, product.CreatedBy);

            result.LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(product.LastModifiedAt, product.LastModifiedBy);

            return result;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);

            if (product == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("Product"));

            _unitOfWork.Products.Delete(product);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new Exception(Errors.DELETE_FAILURE) : true;
        }

        public async Task<ProductDto> GetProduct(Guid productId)
        {
            var product = await _unitOfWork.Products.GetIQueryable()
                .Include(x => x.ProductPhotos).Include(x => x.Category).Include(x => x.Condition).Include(x => x.ProductType)
                .Include(x => x.SubBrand).ThenInclude(s => s!.Brand).FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null) throw new Exception(Errors.RESOURCE_NOTFOUND("Product"));

            var result = _mapper.Map<ProductDto>(product);

            result.CreatedInfo = await _userAccessor.GetCreatedInfo(product.CreatedAt, product.CreatedBy);

            result.LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(product.LastModifiedAt, product.LastModifiedBy);

            for(int i = 0; i < product.ProductPhotos!.Count; i++)
            {
                result.ProductPhotos![i].CreatedInfo =
                    await _userAccessor.GetCreatedInfo(product.ProductPhotos.ToList()[i].CreatedAt,
                    product.ProductPhotos.ToList()[i].CreatedBy);
                result.ProductPhotos![i].LastModifiedInfo =
                    await _userAccessor.GetLastModifiedInfo(product.ProductPhotos.ToList()[i].LastModifiedAt,
                    product.ProductPhotos.ToList()[i].LastModifiedBy);
            }

            return result;
        }

        public async Task<List<ProductDto>> GetProducts()
        {
            var products = await _unitOfWork.Products.GetIQueryable().Include(x => x.ProductPhotos)
                .Include(x => x.Category).Include(x => x.Condition).Include(x => x.ProductType)
                .Include(x => x.SubBrand).ThenInclude(s => s!.Brand).ToListAsync();

            var result = _mapper.Map<List<ProductDto>>(products);

            for (int i = 0; i < products.Count; i++)
            {
                result[i].Category!.CreatedInfo =
                    await _userAccessor.GetCreatedInfo(products[i].Category!.CreatedAt, products[i].Category!.CreatedBy);
                result[i].Category!.LastModifiedInfo =
                    await _userAccessor.GetLastModifiedInfo(products[i].Category!.LastModifiedAt, products[i].Category!.LastModifiedBy);

                result[i].Condition!.CreatedInfo =
                    await _userAccessor.GetCreatedInfo(products[i].Condition!.CreatedAt, products[i].Condition!.CreatedBy);
                result[i].Condition!.LastModifiedInfo =
                    await _userAccessor.GetLastModifiedInfo(products[i].Condition!.LastModifiedAt, products[i].Condition!.LastModifiedBy);

                result[i].SubBrand!.CreatedInfo =
                    await _userAccessor.GetCreatedInfo(products[i].SubBrand!.CreatedAt, products[i].SubBrand!.CreatedBy);
                result[i].SubBrand!.LastModifiedInfo =
                    await _userAccessor.GetLastModifiedInfo(products[i].SubBrand!.LastModifiedAt, products[i].SubBrand!.LastModifiedBy);

                result[i].ProductType!.CreatedInfo =
                    await _userAccessor.GetCreatedInfo(products[i].ProductType!.CreatedAt, products[i].ProductType!.CreatedBy);
                result[i].ProductType!.LastModifiedInfo =
                    await _userAccessor.GetLastModifiedInfo(products[i].ProductType!.LastModifiedAt, products[i].ProductType!.LastModifiedBy);

                result[i].CreatedInfo = await _userAccessor.GetCreatedInfo(products[i].CreatedAt, products[i].CreatedBy);
                result[i].LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(products[i].LastModifiedAt, products[i].LastModifiedBy);

                for (int j = 0; j < products[i].ProductPhotos!.Count; j++)
                {
                    result[i].ProductPhotos![j].CreatedInfo =
                        await _userAccessor.GetCreatedInfo(products[i].ProductPhotos!.ToList()[j].CreatedAt,
                        products[i].ProductPhotos!.ToList()[j].CreatedBy);
                    result[i].ProductPhotos![j].LastModifiedInfo =
                        await _userAccessor.GetLastModifiedInfo(products[i].ProductPhotos!.ToList()[j].LastModifiedAt,
                        products[i].ProductPhotos!.ToList()[j].LastModifiedBy);
                }
            }

            return result;
        }

        public async Task<List<CompactProductDto>> GetProductsPublic()
        {
            var products = await _unitOfWork.Products.GetIQueryable().Include(x => x.ProductPhotos)
                .Include(x => x.Category).Include(x => x.Condition).Include(x => x.ProductType)
                .Include(x => x.SubBrand).ThenInclude(s => s!.Brand).ToListAsync();

            var result = _mapper.Map<List<CompactProductDto>>(products);

            return result;
        }

        public async Task<bool> UpdateProduct(Guid productId, UpdateProductDto productDto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);

            if (product == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("Product"));

            product = _mapper.Map<Product>(productDto);

            product.LastModifiedAt = DateTime.UtcNow;
            product.LastModifiedBy = _userAccessor.GetUserId();

            _unitOfWork.Products.Update(product);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new Exception(Errors.UPDATE_FAILURE) : true;
        }
    }
}
