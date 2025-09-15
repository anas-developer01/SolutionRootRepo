using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerLeadRepository
    {
        Task<CustomerLead?> GetByIdWithImagesAsync(int id, CancellationToken cancellationToken = default);
        Task AddImagesAsync(IEnumerable<CustomerLeadImage> images, CancellationToken cancellationToken = default);
        Task<CustomerLeadImage?> GetImageByIdAsync(int imageId, int customerId, CancellationToken cancellationToken = default);
        Task RemoveImageAsync(CustomerLeadImage image, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
