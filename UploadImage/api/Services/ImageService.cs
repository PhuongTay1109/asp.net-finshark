using MinimalImageUploadAPI.Data;
using MinimalImageUploadAPI.Models;

namespace MinimalImageUploadAPI.Services
{

    public class ImageService : IImageService
    {
        private readonly AppDbContext _context;

        public ImageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ImageModel> UploadImageAsync(IFormFile image, string name)
        {
            if (image == null || image.Length == 0)
                throw new ArgumentException("No image provided.");

            using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            var imageData = memoryStream.ToArray();

            var newImage = new ImageModel
            {
                Name = name,
                ImageData = imageData
            };

            _context.Images.Add(newImage);
            await _context.SaveChangesAsync();

            return newImage;
        }
    }

}
