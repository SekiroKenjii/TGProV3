using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Accessors;
using Core.Constants;
using Core.Enums;
using Core.Wrappers;
using Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Service.Photo
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;
        public PhotoAccessor(IOptions<CloudinarySettings> config)
        {
            var account = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<PhotoHandleResult> AddPhoto(IFormFile file, string gender = "Undefined")
        {
            var uploadParams = new ImageUploadParams();

            if (file == null)
            {
                var result = GetDefaultPhoto(gender);
                return new PhotoHandleResult
                {
                    PublicId = result.DefaultPublicId,
                    PhotoUrl = result.DefaultPhotoUrl
                };
            }

            using var stream = file.OpenReadStream();

            uploadParams.File = new FileDescription(file.FileName, stream);
            uploadParams.Folder = $"Messenger/users/";
            uploadParams.Transformation = new Transformation().Height(500).Crop("fill");
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            return new PhotoHandleResult
            {
                PhotoUrl = uploadResult.SecureUrl.ToString(),
                PublicId = uploadResult.PublicId
            };
        }

        public async Task<string?> DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok" ? result.Result : null;
        }

        private static DefaultPhoto GetDefaultPhoto(string gender)
        {
            if (gender == Gender.Female.ToString())
            {
                return new DefaultPhoto
                {
                    DefaultPhotoUrl = Applications.DEFAUlT_FEMALE_AVATAR,
                    DefaultPublicId = Applications.DEFAUlT_FEMALE_AVATAR_ID
                };
            }

            return new DefaultPhoto
            {
                DefaultPhotoUrl = Applications.DEFAUlT_MALE_AVATAR,
                DefaultPublicId = Applications.DEFAUlT_MALE_AVATAR_ID
            };
        }
    }
}
