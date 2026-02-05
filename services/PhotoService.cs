using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DaringAPI.Helpers;
using DaringAPI.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DaringAPI.services
{
    public class PhotoService : IPhotoService
    {
    private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var coudinaryAccount = new Account(
                config.Value.CloudName, 
                config.Value.ApiKey, 
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(coudinaryAccount);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadPhotoResult = new ImageUploadResult();
            if(file.Length>0){
                            using var stream = file.OpenReadStream();
                            var uploadParams = new ImageUploadParams
                                {
                                    File = new FileDescription(file.FileName, stream), 
                                    Transformation = new Transformation().Height(100)
                                                                            .Width(100)
                                                                            .Crop("fill")
                                                                            .Gravity("face")
                                };
                            uploadPhotoResult = await _cloudinary.UploadAsync(uploadParams);                         
                         }
              return uploadPhotoResult;
        }
        public async Task<DeletionResult> DeletePhotoAsync(string PublicId)
        {
            var deleteParam = new DeletionParams(PublicId);
            var deletePhoto = await _cloudinary.DestroyAsync(deleteParam);
            return deletePhoto;
        }
    }
}