using AutoMapper;
using Core;
using Core.Accessors;
using Core.Constants;
using Core.DTOs.Product;
using Core.Exceptions;
using Core.Services;
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

        public async Task<List<ProductPhotoDto>> AddProductPhotos(AddProductPhotosDto producPhotoDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteProductPhoto(Guid productPhotoId)
        {
            var productPhoto = await _unitOfWork.ProductPhotos.GetByIdAsync(productPhotoId);

            if (productPhoto == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("Product Photo"));

            if (productPhoto.ProductPhotoId == Applications.DEFAUlT_PRODUCT_PHOTO_ID)
            {
                throw new BadRequestException("You can not delete default product photo");
            }

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
            throw new NotImplementedException();
        }

        public async Task<List<ProductPhotoDto>> UpdateProductPhotos(UpdateProductPhotosDto producPhotoDto)
        {
            throw new NotImplementedException();
        }
    }
}
