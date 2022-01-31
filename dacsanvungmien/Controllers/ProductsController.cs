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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace dacsanvungmien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository repository;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductsController(IProductRepository repository, IWebHostEnvironment hostEnvironment)
        {
            this.repository = repository;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetProduct()
        {
            return (await repository.GetProductsAsync()).Select(item => item.AsDto());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await repository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product.AsDto();
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDto productDto)
        {
            var product = await repository.GetProductByIdAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.CategoryId = productDto.CategoryId;
            product.Price = productDto.Price;
            product.PriceSale = productDto.PriceSale;
            product.RegionId = productDto.RegionId;
            await repository.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(CreateProductDto productDto)
        {
            Product product = new()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                PriceSale = productDto.PriceSale,
                RegionId = productDto.RegionId,
                CategoryId = productDto.CategoryId,
            };
            await repository.AddProductAsync(product);
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await repository.DeleteProductAsync(id);
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
