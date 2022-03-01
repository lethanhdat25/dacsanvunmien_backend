using dacsanvungmien.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private DacSanVungMienContext context;
        public ProductRepository(DacSanVungMienContext context)
        {
            this.context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            var category = await context.Category.FindAsync(product.CategoryId);
            var region = await context.Region.FindAsync(product.RegionId);
            if (category is null || region is null) return null;
            await context.Product.AddAsync(product);
            await SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(int id)
        {
            var productImage = await context.ProductImage.FirstOrDefaultAsync(x => x.ProductId == id);
            while (productImage!=null)
            {
                var image = await context.ProductImage.FirstOrDefaultAsync(x => x.ProductId == id);
               if (image == null) break;
                context.ProductImage.Remove(image);
                await SaveChangesAsync();
            }
            Product product = await context.Product.FindAsync(id);
            if (product != null)
            {
                context.Product.Remove(product);
                await SaveChangesAsync();
            }

        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await context.Product.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await context.Product.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
