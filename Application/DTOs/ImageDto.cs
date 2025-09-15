using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string Base64Data { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
