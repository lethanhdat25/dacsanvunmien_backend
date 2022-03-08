using dacsanvungmien.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private DacSanVungMienContext context;
        public ProductImageRepository(DacSanVungMienContext context)
        {
            this.context = context;
        }
        public async Task<ProductImage> AddProductImageAsync(ProductImage productImage)
        {
            var product = await context.Product.FindAsync(productImage.ProductId);
            if (product is null) return null;
            await context.ProductImage.AddAsync(productImage);
            await SaveChangesAsync();
            return productImage;
        }

        public async Task DeleteProductImageAsync(int id)
        {
            var productImage = await context.ProductImage.FirstOrDefaultAsync(x => x.ProductId == id);
            while (productImage != null)
            {
                var image = await context.ProductImage.FirstOrDefaultAsync(x => x.ProductId == id);
                if (image == null) break;
                context.ProductImage.Remove(image);
                await SaveChangesAsync();
            }
        }

        public async Task<ProductImage> GetProductImageByIdAsync(int id)
        {
            return await context.ProductImage.FindAsync(id);
        }

        public async Task<IEnumerable<ProductImage>> GetProductImagesAsync()
        {
            return await context.ProductImage.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateProductImageAsync(ProductImage productImage)
        {
            context.Entry(productImage).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
