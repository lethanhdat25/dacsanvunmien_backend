using dacsanvungmien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<ProductImage>> GetProductImagesAsync();
        Task<ProductImage> GetProductImageByIdAsync(int id);
        Task<ProductImage> AddProductImageAsync(ProductImage productImage);
        Task DeleteProductImageAsync(int id);
        Task UpdateProductImageAsync(ProductImage productImage);
        Task SaveChangesAsync();
    }
}
