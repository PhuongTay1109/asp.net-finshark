using MinimalImageUploadAPI.Models;

namespace MinimalImageUploadAPI.Services
{
    public interface IImageService
    {
         Task<ImageModel> UploadImageAsync(IFormFile image, string name);        
    }
}