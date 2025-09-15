using Api.Models;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Exceptions;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/images")]
    public class CustomerLeadImagesController : ControllerBase
    {
        private readonly ICustomerLeadService _service;

        public CustomerLeadImagesController(ICustomerLeadService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetImages(int customerId)
        {
            try
            {
                var images = await _service.GetImagesAsync(customerId);
                var response = images.Select(i => new ImageResponse { Id = i.Id, Base64Data = i.Base64Data, CreatedAt = i.CreatedAt });
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImages(int customerId, [FromForm] UploadFilesRequest request)
        {
            try
            {
                var added = await _service.UploadImagesAsync(customerId, request.Files);
                var response = added.Select(i => new ImageResponse
                {
                    Id = i.Id,
                    Base64Data = i.Base64Data,
                    CreatedAt = i.CreatedAt
                });

                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeleteImage(int customerId, int imageId)
        {
            try
            {
                await _service.DeleteImageAsync(customerId, imageId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
