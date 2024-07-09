namespace MinimalImageUploadAPI.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
         public string Name { get; set; } = string.Empty; 

        public byte[] ImageData { get; set; } = Array.Empty<byte>();  
    }
}
