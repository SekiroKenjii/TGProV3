using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Accessors;
using Core.Constants;
using Domain.Enums;
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

        public async Task<PhotoHandleResult> AddPhoto(IFormFile file, string entity, string? gender = null)
        {
            var uploadParams = new ImageUploadParams();

            if (file == null)
            {
                var result = entity switch
                {
                    Applications.USER => GetDefaultPhoto(entity, gender),
                    Applications.BRAND => GetDefaultPhoto(entity),
                    Applications.PRODUCT => GetDefaultPhoto(entity),
                    _ => null
                };

                return new PhotoHandleResult
                {
                    PublicId = result!.DefaultPublicId,
                    PhotoUrl = result!.DefaultPhotoUrl
                };
            }

            using var stream = file!.OpenReadStream();

            uploadParams.Folder = $"TGProV3/{entity}/";

            uploadParams.File = new FileDescription(file.FileName, stream);

            uploadParams.Transformation = entity switch
            {
                Applications.USER => new Transformation().Height(500).Crop("fill"),
                Applications.BRAND => new Transformation().Height(500).Crop("fill"),
                Applications.PRODUCT => new Transformation().Height(800).Width(800).Crop("fill"),
                _ => new Transformation().Height(500).Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null) throw new Exception(uploadResult.Error.Message);

            return new PhotoHandleResult
            {
                PhotoUrl = uploadResult.SecureUrl.ToString(),
                PublicId = uploadResult.PublicId
            };
        }

        public async Task<string> DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result;
        }

        private static DefaultPhoto? GetDefaultPhoto(string entity, string? gender = null)
        {
            switch (entity)
            {
                case Applications.USER:
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
                case Applications.BRAND:
                    return new DefaultPhoto
                    {
                        DefaultPhotoUrl = Applications.DEFAUlT_BRAND_PHOTO,
                        DefaultPublicId = Applications.DEFAUlT_BRAND_PHOTO_ID
                    };
                case Applications.PRODUCT:
                    return new DefaultPhoto
                    {
                        DefaultPhotoUrl = Applications.DEFAUlT_PRODUCT_PHOTO,
                        DefaultPublicId = Applications.DEFAUlT_PRODUCT_PHOTO_ID
                    };
            }

            return null;
        }
    }
}
