using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using InSynq.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;

namespace InSynq.Infrastructure.Storage;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService() => _cloudinary = new(new Account(Common.Settings.CloudStorageName, Common.Settings.CloudStorageApiKey, Common.Settings.CloudStorageApiSecret));

    public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file)
    {
        if (file.Length < 1)
            return new();

        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation().Height(300).Width(300).Crop("fill").Gravity("face"),
            Folder = Common.Constants.CLOUDINARY_STORAGE_NAME
        };

        return await _cloudinary.UploadAsync(uploadParams);
    }

    public async Task<DeletionResult> DeletePhotoAsync(string publicId) => await _cloudinary.DestroyAsync(new DeletionParams(publicId));
}