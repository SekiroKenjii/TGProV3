using AutoMapper;
using Core;
using Core.Accessors;
using Core.Constants;
using Core.DTOs.Condition;
using Core.Exceptions;
using Core.Services;
using Domain.Entities;

namespace Service
{
    public class ConditionService : IConditionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public ConditionService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<ConditionDto> AddCondition(AddConditionDto conditionDto)
        {
            var condition = _mapper.Map<Condition>(conditionDto);

            condition.CreatedBy = _userAccessor.GetUserId();

            await _unitOfWork.Conditions.AddAsync(condition);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            if (!isSaved) throw new BadRequestException(Messages.ADD_FAILURE);

            var result = _mapper.Map<ConditionDto>(condition);

            result.CreatedInfo = await _userAccessor.GetCreatedInfo(condition.CreatedAt, condition.CreatedBy);

            result.LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(condition.LastModifiedAt, condition.LastModifiedBy);

            return result;
        }

        public async Task<bool> DeleteCondition(Guid conditionId)
        {
            var condition = await _unitOfWork.Conditions.GetByIdAsync(conditionId);

            if (condition == null) throw new NotFoundException(Messages.RESOURCE_NOTFOUND("Condition"));

            _unitOfWork.Conditions.Delete(condition);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new BadRequestException(Messages.DELETE_FAILURE) : true;
        }

        public async Task<ConditionDto> GetCondition(Guid conditionId)
        {
            var condition = await _unitOfWork.Conditions.GetByIdAsync(conditionId);

            if (condition == null) throw new NotFoundException(Messages.RESOURCE_NOTFOUND("Condition"));

            var result = _mapper.Map<ConditionDto>(condition);

            result.CreatedInfo = await _userAccessor.GetCreatedInfo(condition.CreatedAt, condition.CreatedBy);

            result.LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(condition.LastModifiedAt, condition.LastModifiedBy);

            return result;
        }

        public async Task<List<ConditionDto>> GetConditions()
        {
            var conditions = await _unitOfWork.Conditions.GetAllAsync();

            var result = _mapper.Map<List<ConditionDto>>(conditions);

            for (int i = 0; i < conditions.Count; i++)
            {
                result[i].CreatedInfo = await _userAccessor.GetCreatedInfo(conditions[i].CreatedAt, conditions[i].CreatedBy);
                result[i].LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(conditions[i].LastModifiedAt, conditions[i].LastModifiedBy);
            }

            return result;
        }

        public async Task<List<CompactConditionDto>> GetConditionsPublic()
        {
            var conditions = await _unitOfWork.Conditions.GetAllAsync();

            var result = _mapper.Map<List<CompactConditionDto>>(conditions);

            return result;
        }

        public async Task<bool> UpdateCondition(Guid conditionId, UpdateConditionDto conditionDto)
        {
            var condition = await _unitOfWork.Conditions.GetByIdAsync(conditionId);

            if (condition == null) throw new NotFoundException(Messages.RESOURCE_NOTFOUND("Condition"));

            condition.Name = conditionDto.Name;
            condition.Description = conditionDto.Description;
            condition.LastModifiedAt = DateTime.UtcNow;
            condition.LastModifiedBy = _userAccessor.GetUserId();

            _unitOfWork.Conditions.Update(condition);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new BadRequestException(Messages.UPDATE_FAILURE) : true;
        }
    }
}
