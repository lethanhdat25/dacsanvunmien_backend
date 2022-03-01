using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dacsanvungmien.Dtos;
using dacsanvungmien.Models;
using dacsanvungmien.Repositories;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace dacsanvungmien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepository repository;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductImageController(IProductImageRepository repository, IWebHostEnvironment hostEnvironment)
        {
            this.repository = repository;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: api/ProductImage
        [HttpGet]
        public async Task<IEnumerable<ProductImageDto>> GetProductImage()
        {
            return (await repository.GetProductImagesAsync()).Select(item => {
                var imageSrc = FormatImageSrc(item.Image);
                return item.AsDto(imageSrc);
            });
        }

        // GET: api/ProductImage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImageDto>> GetProductImage(int id)
        {
            var productImage = await repository.GetProductImageByIdAsync(id);
            var imageSrc = FormatImageSrc(productImage.Image);

            if (productImage == null)
            {
                return NotFound();
            }

            return productImage.AsDto(imageSrc);
        }

        // PUT: api/ProductImage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> PutProductImage(int id,[FromForm] UpdateProductImageDto productImageDto)
        {
            var productImage = await repository.GetProductImageByIdAsync(id);
            if (productImage is null)
            {
                return NotFound();
            }
            foreach(var image in productImageDto.Image)
            {
                productImage.ProductId = productImageDto.ProductId;
                productImage.Image = await SaveImage(image);
            }
            
            await repository.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ProductImage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ProductImageDto>> PostProductImage([FromForm]CreateProductImageDto productImageDto)
        {
             foreach (var image in productImageDto.Image)
            {
                ProductImage productImage = new()
                {
                    Image = await SaveImage(image),
                    ProductId = productImageDto.ProductId
                };
                await repository.AddProductImageAsync(productImage);
            }
            return Ok("Post Image Fulfilled");
        }

        // DELETE: api/ProductImage/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteProductImageDto(int id)
        {
            var productImage = await repository.GetProductImageByIdAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }

            await repository.DeleteProductImageAsync(id);
            return NoContent();
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(hostEnvironment.ContentRootPath, "Uploads", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
        [NonAction]
        private string FormatImageSrc(string image)
        {
            return String.Format("{0}://{1}{2}/Uploads/{3}", Request.Scheme, Request.Host, Request.PathBase, image);
        }
    }
}
