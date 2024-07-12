using System.ComponentModel.DataAnnotations;

namespace MinimalImageUploadAPI.Models
{
    public class ImageModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty; 
        public byte[] ImageData { get; set; } = [];  
    }
}
