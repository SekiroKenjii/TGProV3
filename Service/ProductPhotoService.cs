using AutoMapper;
using Core;
using Core.Accessors;
using Core.Constants;
using Core.DTOs.Product;
using Core.Exceptions;
using Core.Helpers;
using Core.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class ProductPhotoService : IProductPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IPhotoAccessor _photoAccessor;
        public ProductPhotoService(IUnitOfWork unitOfWork, IMapper mapper,
            IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _photoAccessor = photoAccessor;
        }

        public async Task<bool> AddProductPhotos(AddProductPhotosDto producPhotoDto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(producPhotoDto.ProductId);

            var productPhotos = await _unitOfWork.ProductPhotos.GetIQueryable()
                .Where(x => x.ProductId == producPhotoDto.ProductId).ToListAsync();

            if(product == null || productPhotos.Count == 0)
                throw new NotFoundException(Errors.RESOURCE_NOTFOUND("Product Or Product Photo"));

            bool isSaved = false;

            if (productPhotos.Count == 1 && productPhotos[0].ProductPhotoId == Applications.DEFAUlT_PRODUCT_PHOTO_ID)
            {
                _unitOfWork.ProductPhotos.Delete(productPhotos[0]);

                for (int i = 0; i < producPhotoDto.Photos!.Count; i++)
                {
                    var uploadProductPhoto = await _photoAccessor.AddPhoto(producPhotoDto.Photos[i], Applications.PRODUCT);

                    int sortOrder = i + 1;

                    product.ProductPhotos!.Add(new ProductPhoto
                    {
                        ProductPhotoUrl = uploadProductPhoto.PhotoUrl,
                        ProductPhotoId = uploadProductPhoto.PublicId,
                        Caption = product.Name!.GeneratePhotoCaption(sortOrder),
                        SortOrder = sortOrder
                    });

                    _unitOfWork.Products.Update(product);
                }

                isSaved = await _unitOfWork.SaveChangeAsync() > 0;

                return !isSaved ? throw new Exception(Errors.ADD_FAILURE) : true;
            }

            for (int i = 0; i < producPhotoDto.Photos!.Count; i++)
            {
                var uploadProductPhoto = await _photoAccessor.AddPhoto(producPhotoDto.Photos[i], Applications.PRODUCT);

                int sortOrder = productPhotos[^1].SortOrder + i + 1;

                product.ProductPhotos!.Add(new ProductPhoto
                {
                    ProductPhotoUrl = uploadProductPhoto.PhotoUrl,
                    ProductPhotoId = uploadProductPhoto.PublicId,
                    Caption = product.Name!.GeneratePhotoCaption(sortOrder),
                    SortOrder = sortOrder
                });

                _unitOfWork.Products.Update(product);
            }

            isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new Exception(Errors.ADD_FAILURE) : true;
        }

        public async Task<bool> DeleteProductPhoto(Guid productPhotoId)
        {
            var productPhoto = await _unitOfWork.ProductPhotos.GetByIdAsync(productPhotoId);

            if (productPhoto == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("Product Photo"));

            if (productPhoto.ProductPhotoId == Applications.DEFAUlT_PRODUCT_PHOTO_ID)
                throw new BadRequestException("You can not delete default product photo");

            var deleteProductPhoto = await _photoAccessor.DeletePhoto(productPhoto.ProductPhotoId!);

            if (deleteProductPhoto != "ok") throw new Exception(deleteProductPhoto);

            _unitOfWork.ProductPhotos.Delete(productPhoto);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new Exception(Errors.DELETE_FAILURE) : true;
        }

        public async Task<List<ProductPhotoDto>> GetProductPhotos(Guid productId)
        {
            var productPhotos = await _unitOfWork.ProductPhotos.GetIQueryable()
                .Where(x => x.ProductId == productId).ToListAsync();

            var result = _mapper.Map<List<ProductPhotoDto>>(productPhotos);

            for (int i = 0; i < productPhotos.Count; i++)
            {
                result[i].CreatedInfo = await _userAccessor.GetCreatedInfo(productPhotos[i].CreatedAt, productPhotos[i].CreatedBy);
                result[i].LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(productPhotos[i].LastModifiedAt, productPhotos[i].LastModifiedBy);
            }

            return result;
        }

        public async Task<bool> UpdateProductPhoto(Guid productPhotoId, UpdateProductPhotoDto producPhotoDto)
        {
            var productPhoto = await _unitOfWork.ProductPhotos.GetByIdAsync(productPhotoId);

            if (productPhoto == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("Product Photo"));

            if (productPhoto.ProductPhotoId != Applications.DEFAUlT_PRODUCT_PHOTO_ID)
            {
                var deleteProductPhoto = await _photoAccessor.DeletePhoto(productPhoto.ProductPhotoId!);

                if (deleteProductPhoto != "ok") throw new Exception(deleteProductPhoto);
            }

            var uploadProductPhoto = await _photoAccessor.AddPhoto(producPhotoDto.Photo!, Applications.PRODUCT);

            productPhoto.ProductPhotoId = uploadProductPhoto.PublicId;
            productPhoto.ProductPhotoUrl = uploadProductPhoto.PhotoUrl;

            _unitOfWork.ProductPhotos.Update(productPhoto);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new Exception(Errors.UPDATE_FAILURE) : true;
        }
    }
}
