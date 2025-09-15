using Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerLeadService
    {
        Task<IEnumerable<ImageDto>> GetImagesAsync(int customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ImageDto>> UploadImagesAsync(int customerId, List<IFormFile> files, CancellationToken cancellationToken = default);
        Task DeleteImageAsync(int customerId, int imageId, CancellationToken cancellationToken = default);
    }
}
