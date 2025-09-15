namespace Api.Models
{
    public class ImageResponse
    {
        public int Id { get; set; }
        public string Base64Data { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
