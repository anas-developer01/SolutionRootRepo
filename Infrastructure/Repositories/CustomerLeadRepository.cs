using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CustomerLeadRepository : ICustomerLeadRepository
    {
        private readonly AppDbContext _ctx;

        public CustomerLeadRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<CustomerLead?> GetByIdWithImagesAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _ctx.CustomerLeads.Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task AddImagesAsync(IEnumerable<CustomerLeadImage> images, CancellationToken cancellationToken = default)
        {
            await _ctx.CustomerLeadImages.AddRangeAsync(images, cancellationToken);
        }

        public async Task<CustomerLeadImage?> GetImageByIdAsync(int imageId, int customerId, CancellationToken cancellationToken = default)
        {
            return await _ctx.CustomerLeadImages
                .FirstOrDefaultAsync(i => i.Id == imageId && i.CustomerLeadId == customerId, cancellationToken);
        }

        public async Task RemoveImageAsync(CustomerLeadImage image, CancellationToken cancellationToken = default)
        {
            _ctx.CustomerLeadImages.Remove(image);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _ctx.SaveChangesAsync(cancellationToken);
        }
    }
}