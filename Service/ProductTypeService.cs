using AutoMapper;
using Core;
using Core.Accessors;
using Core.Constants;
using Core.DTOs.ProductType;
using Core.Exceptions;
using Core.Services;
using Domain.Entities;

namespace Service
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public ProductTypeService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<ProductTypeDto> AddProductType(AddProductTypeDto productTypeDto)
        {
            var productType = _mapper.Map<ProductType>(productTypeDto);

            productType.CreatedBy = _userAccessor.GetUserId();

            await _unitOfWork.ProductTypes.AddAsync(productType);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            if (!isSaved) throw new Exception(Errors.ADD_FAILURE);

            var result = _mapper.Map<ProductTypeDto>(productType);

            result.CreatedInfo = await _userAccessor.GetCreatedInfo(productType.CreatedAt, productType.CreatedBy);

            result.LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(productType.LastModifiedAt, productType.LastModifiedBy);

            return result;
        }

        public async Task<bool> DeleteProductType(Guid productTypeId)
        {
            var productType = await _unitOfWork.ProductTypes.GetByIdAsync(productTypeId);

            if (productType == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("Product Type"));

            _unitOfWork.ProductTypes.Delete(productType);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new Exception(Errors.DELETE_FAILURE) : true;
        }

        public async Task<ProductTypeDto> GetProductType(Guid productTypeId)
        {
            var productType = await _unitOfWork.ProductTypes.GetByIdAsync(productTypeId);

            if (productType == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("Product Type"));

            var result = _mapper.Map<ProductTypeDto>(productType);

            result.CreatedInfo = await _userAccessor.GetCreatedInfo(productType.CreatedAt, productType.CreatedBy);

            result.LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(productType.LastModifiedAt, productType.LastModifiedBy);

            return result;
        }

        public async Task<List<ProductTypeDto>> GetProductTypes()
        {
            var productTypes = await _unitOfWork.ProductTypes.GetAllAsync();

            var result = _mapper.Map<List<ProductTypeDto>>(productTypes);

            for (int i = 0; i < productTypes.Count; i++)
            {
                result[i].CreatedInfo = await _userAccessor.GetCreatedInfo(productTypes[i].CreatedAt, productTypes[i].CreatedBy);
                result[i].LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(productTypes[i].LastModifiedAt, productTypes[i].LastModifiedBy);
            }

            return result;
        }

        public async Task<List<CompactProductTypeDto>> GetProductTypesPublic()
        {
            var productTypes = await _unitOfWork.ProductTypes.GetAllAsync();

            var result = _mapper.Map<List<CompactProductTypeDto>>(productTypes);

            return result;
        }

        public async Task<bool> UpdateProductType(Guid productTypeId, UpdateProductTypeDto productTypeDto)
        {
            var productType = await _unitOfWork.ProductTypes.GetByIdAsync(productTypeId);

            if (productType == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("Product Type"));

            productType.Name = productTypeDto.Name;
            productType.Description = productTypeDto.Description;
            productType.LastModifiedAt = DateTime.UtcNow;
            productType.LastModifiedBy = _userAccessor.GetUserId();

            _unitOfWork.ProductTypes.Update(productType);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new Exception(Errors.UPDATE_FAILURE) : true;
        }
    }
}
