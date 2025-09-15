using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;  
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerLeadService : ICustomerLeadService
    {
        private const int MAX_IMAGES = 10;
        private readonly ICustomerLeadRepository _repo;

        public CustomerLeadService(ICustomerLeadRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ImageDto>> GetImagesAsync(int customerId, CancellationToken cancellationToken = default)
        {
            var customer = await _repo.GetByIdWithImagesAsync(customerId, cancellationToken);
            if (customer == null)
                throw new NotFoundException("Customer/Lead not found.");

            return customer.Images
                .OrderByDescending(i => i.CreatedAt)
                .Select(i => new ImageDto { Id = i.Id, Base64Data = i.Base64Data, CreatedAt = i.CreatedAt })
                .ToList();
        }

         
        public async Task<IEnumerable<ImageDto>> UploadImagesAsync(int customerId, List<IFormFile> files, CancellationToken cancellationToken = default)
        {
            var customer = await _repo.GetByIdWithImagesAsync(customerId, cancellationToken);
            if (customer == null)
                throw new NotFoundException("Customer/Lead not found.");

            if (files == null || !files.Any())
                throw new BadRequestException("No files provided in request.");

            var currentCount = customer.Images.Count;
            if (currentCount + files.Count > MAX_IMAGES)
            {
                var allowed = MAX_IMAGES - currentCount;
                throw new BadRequestException($"Upload would exceed the limit of {MAX_IMAGES} images. You can upload up to {allowed} more image(s).");
            }

            var toAdd = new List<CustomerLeadImage>();

            foreach (var file in files)
            {
                if (file.Length == 0)
                    continue;

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream, cancellationToken);

                var base64String = Convert.ToBase64String(memoryStream.ToArray());

                toAdd.Add(new CustomerLeadImage
                {
                    Base64Data = base64String,
                    CustomerLeadId = customerId, // Ensure FK is set
                    CreatedAt = DateTime.UtcNow
                });
            }

            if (!toAdd.Any())
                throw new BadRequestException("Uploaded files are empty or invalid.");

            await _repo.AddImagesAsync(toAdd, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);

            // Return the freshly added images
            return toAdd.Select(i => new ImageDto
            {
                Id = i.Id,
                Base64Data = i.Base64Data,
                CreatedAt = i.CreatedAt
            });
        }

        public async Task DeleteImageAsync(int customerId, int imageId, CancellationToken cancellationToken = default)
        {
            var image = await _repo.GetImageByIdAsync(imageId, customerId, cancellationToken);
            if (image == null)
                throw new NotFoundException("Image not found for this customer/lead.");

            await _repo.RemoveImageAsync(image, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);
        }
    }
}
