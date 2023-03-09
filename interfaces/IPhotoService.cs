using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace DaringAPI.interfaces
{
    public interface IPhotoService
    {
         Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
         Task<DeletionResult> DeletePhotoAsync(string PublicId);
    }
}