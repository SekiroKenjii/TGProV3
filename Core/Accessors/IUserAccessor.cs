using Core.DTOs.Auditable;

namespace Core.Accessors
{
    public interface IUserAccessor
    {
        Guid GetUserId();
        string GetUserEmail();
        Task<CreatedByDto?> GetCreatedInfo(DateTime CreatedAt, Guid userId);
        Task<LastModifiedByDto?> GetLastModifiedInfo(DateTime? LastModifiedAt, Guid? userId = default);
    }
}
