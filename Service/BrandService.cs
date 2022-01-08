using AutoMapper;
using Core;
using Core.Accessors;
using Core.Constants;
using Core.DTOs.Brand;
using Core.Exceptions;
using Core.Services;
using Domain.Entities;

namespace Service
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IPhotoAccessor _photoAccessor;
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper,
            IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _photoAccessor = photoAccessor;
        }

        public async Task<BrandDto> AddBrand(AddBrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);

            brand.CreatedBy = _userAccessor.GetUserId();

            var brandPhoto = await _photoAccessor.AddPhoto(brandDto.Photo!, Applications.BRAND.ToString());

            brand.LogoId = brandPhoto.PublicId;
            brand.LogoUrl = brandPhoto.PhotoUrl;

            await _unitOfWork.Brands.AddAsync(brand);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            if (!isSaved) throw new Exception(Errors.ADD_FAILURE);

            var result = _mapper.Map<BrandDto>(brand);

            result.CreatedInfo = await _userAccessor.GetCreatedInfo(brand.CreatedAt, brand.CreatedBy);

            result.LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(brand.LastModifiedAt, brand.LastModifiedBy);

            return result;
        }

        public async Task<bool> DeleteBrand(Guid brandId)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(brandId);

            if (brand == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("Brand"));

            _unitOfWork.Brands.Delete(brand);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            if (!isSaved) throw new Exception(Errors.DELETE_FAILURE);

            if (brand.LogoId != Applications.DEFAUlT_BRAND_PHOTO_ID)
            {
                await _photoAccessor.DeletePhoto(brand.LogoId!);
            }

            return true;
        }

        public async Task<BrandDto> GetBrand(Guid brandId)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(brandId);

            if (brand == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("Brand"));

            var result = _mapper.Map<BrandDto>(brand);

            result.CreatedInfo = await _userAccessor.GetCreatedInfo(brand.CreatedAt, brand.CreatedBy);

            result.LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(brand.LastModifiedAt, brand.LastModifiedBy);

            return result;
        }

        public async Task<List<CompactBrandDto>> GetBrandsPublic()
        {
            var brands = await _unitOfWork.Brands.GetAllAsync();

            var result = _mapper.Map<List<CompactBrandDto>>(brands);

            return result;
        }

        public async Task<List<BrandDto>> GetBrands()
        {
            var brands = await _unitOfWork.Brands.GetAllAsync();

            var result = _mapper.Map<List<BrandDto>>(brands);

            for (int i = 0; i < brands.Count; i++)
            {
                result[i].CreatedInfo = await _userAccessor.GetCreatedInfo(brands[i].CreatedAt, brands[i].CreatedBy);
                result[i].LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(brands[i].LastModifiedAt, brands[i].LastModifiedBy);
            }

            return result;
        }

        public async Task<bool> UpdateBrand(Guid brandId, UpdateBrandDto brandDto)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(brandId);

            if (brand == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("Brand"));

            brand.Name = brandDto.Name;
            brand.Description = brandDto.Description;
            brand.LastModifiedAt = DateTime.UtcNow;
            brand.LastModifiedBy = _userAccessor.GetUserId();

            if (brandDto.Photo != null)
            {
                if (brand.LogoId != Applications.DEFAUlT_BRAND_PHOTO_ID)
                {
                    var deleteBrandPhoto = await _photoAccessor.DeletePhoto(brand.LogoId!);
                    if (deleteBrandPhoto != "ok") throw new Exception(deleteBrandPhoto);
                }                

                var brandPhoto = await _photoAccessor.AddPhoto(brandDto.Photo, Applications.BRAND);

                brand.LogoId = brandPhoto.PublicId;
                brand.LogoUrl = brandPhoto.PhotoUrl;
            }

            _unitOfWork.Brands.Update(brand);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new Exception(Errors.UPDATE_FAILURE) : true;
        }
    }
}
