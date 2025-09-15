using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Application.DTOs
{
    public class UploadFilesRequest
    {
         
            public List<IFormFile> Files { get; set; } = new();
        
    }
}
