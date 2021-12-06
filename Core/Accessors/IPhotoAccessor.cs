using Core.Wrappers;
using Microsoft.AspNetCore.Http;

namespace Core.Accessors
{
    public interface IPhotoAccessor
    {
        Task<PhotoHandleResult> AddPhoto(IFormFile file, string gender);
        Task<string?> DeletePhoto(string publicId);
    }
}
