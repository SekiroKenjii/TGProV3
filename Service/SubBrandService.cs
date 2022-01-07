using AutoMapper;
using Core;
using Core.Accessors;
using Core.Constants;
using Core.DTOs.SubBrand;
using Core.Exceptions;
using Core.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class SubBrandService : ISubBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public SubBrandService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<SubBrandDto> AddSubBrand(AddSubBrandDto subBrandDto)
        {
            var subBrand = _mapper.Map<SubBrand>(subBrandDto);

            subBrand.CreatedBy = _userAccessor.GetUserId();

            await _unitOfWork.SubBrands.AddAsync(subBrand);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            if (!isSaved) throw new Exception(Errors.ADD_FAILURE);

            var result = _mapper.Map<SubBrandDto>(subBrand);

            result.CreatedInfo = await _userAccessor.GetCreatedInfo(subBrand.CreatedAt, subBrand.CreatedBy);

            result.LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(subBrand.LastModifiedAt, subBrand.LastModifiedBy);

            return result;
        }

        public async Task<bool> DeleteSubBrand(Guid subBrandId)
        {
            var subBrand = await _unitOfWork.SubBrands.GetByIdAsync(subBrandId);

            if (subBrand == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("SubBrand"));

            _unitOfWork.SubBrands.Delete(subBrand);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            if (!isSaved) throw new Exception(Errors.DELETE_FAILURE);

            return true;
        }

        public async Task<SubBrandDto> GetSubBrand(Guid subBrandId)
        {
            var subBrand = await _unitOfWork.SubBrands.GetIQueryable()
                .Include(x => x.Category).Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == subBrandId);

            if (subBrand == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("SubBrand"));

            var result = _mapper.Map<SubBrandDto>(subBrand);

            result.CreatedInfo = await _userAccessor.GetCreatedInfo(subBrand.CreatedAt, subBrand.CreatedBy);

            result.LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(subBrand.LastModifiedAt, subBrand.LastModifiedBy);

            return result;
        }

        public async Task<List<SubBrandDto>> GetSubBrands()
        {
            var subBrands = await _unitOfWork.SubBrands.GetIQueryable()
                .Include(x => x.Category).Include(x => x.Brand).ToListAsync();

            var result = _mapper.Map<List<SubBrandDto>>(subBrands);

            for (int i = 0; i < subBrands.Count; i++)
            {
                result[i].CreatedInfo = await _userAccessor.GetCreatedInfo(subBrands[i].CreatedAt, subBrands[i].CreatedBy);
                result[i].LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(subBrands[i].LastModifiedAt, subBrands[i].LastModifiedBy);
            }

            return result;
        }

        public async Task<List<CompactSubBrandDto>> GetSubBrandsPublic()
        {
            var subBrands = await _unitOfWork.SubBrands.GetIQueryable()
                .Include(x => x.Category).Include(x => x.Brand).ToListAsync();

            var result = _mapper.Map<List<CompactSubBrandDto>>(subBrands);

            return result;
        }

        public async Task<bool> UpdateSubBrand(Guid subBrandId, UpdateSubBrandDto subBrandDto)
        {
            var subBrand = await _unitOfWork.SubBrands.GetByIdAsync(subBrandId);

            if (subBrand == null) throw new NotFoundException(Errors.RESOURCE_NOTFOUND("SubBrand"));

            subBrand.Name = subBrandDto.Name;
            subBrand.Description = subBrandDto.Description;
            subBrand.BrandId = subBrandDto.BrandId;
            subBrand.CategoryId = subBrandDto.CategoryId;
            subBrand.LastModifiedAt = DateTime.UtcNow;
            subBrand.LastModifiedBy = _userAccessor.GetUserId();

            _unitOfWork.SubBrands.Update(subBrand);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new Exception(Errors.UPDATE_FAILURE) : true;
        }
    }
}