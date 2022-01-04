using Core.DTOs.Condition;

namespace Core.Services
{
    public interface IConditionService
    {
        Task<ConditionDto> GetCondition(Guid conditionId);
        Task<List<ConditionDto>> GetConditions();
        Task<ConditionDto> AddCondition(AddConditionDto conditionDto);
        Task<bool> UpdateCondition(Guid conditionId, UpdateConditionDto conditionDto);
        Task<bool> DeleteCondition(Guid conditionId);
    }
}
