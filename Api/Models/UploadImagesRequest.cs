namespace Api.Models
{
    public class UploadImagesRequest
    {
        // Base64 strings
        public List<string> Base64Images { get; set; } = new List<string>();
    }
}
