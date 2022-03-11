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
using Microsoft.AspNetCore.Authorization;

namespace dacsanvungmien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private DacSanVungMienContext context;
        private readonly IProductRepository repository;

        public ProductsController(IProductRepository repository, DacSanVungMienContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        public  IEnumerable<object> GetProduct()
        {

            var productWithImage= from product in context.Product
                                    join productImage in context.ProductImage on product.Id equals productImage.ProductId into gj
                                    from subImage in gj.DefaultIfEmpty()
                                    select new { Product = product, Image = subImage==null?String.Empty:(subImage.Image??String.Empty) };
            return productWithImage;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<object> GetProduct(int id)
        {
            var product = await repository.GetProductByIdAsync(id);
            var Image = from image in context.ProductImage
                                   where product.Id == image.ProductId
                                   select image.Image;
            if (product == null)
            {
                return NotFound();
            }

            return (new { Product = product, Image = Image }); ;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
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
            product.Amount= productDto.Amount;
            product.Dvt = productDto.Dvt;
            product.Weight = productDto.Weight;
            product.RegionId = productDto.RegionId;
            product.ProvinceId=productDto.ProvinceId;
            await repository.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ProductDto>> PostProduct(CreateProductDto productDto)
        {
            Product product = new()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                PriceSale = productDto.PriceSale,
                Amount = productDto.Amount,
                Dvt = productDto.Dvt,
                Weight = productDto.Weight,
                RegionId = productDto.RegionId,
                CategoryId = productDto.CategoryId,
                ProvinceId = productDto.ProvinceId,
                CreatedDate= DateTime.UtcNow
        };
            await repository.AddProductAsync(product);
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
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
        
        
    }
}
