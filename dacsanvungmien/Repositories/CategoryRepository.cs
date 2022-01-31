using dacsanvungmien.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private DacSanVungMienContext context;
        public CategoryRepository(DacSanVungMienContext context)
        {
            this.context = context;
        }
        public async Task<Category> AddCategoryAsync(Category category)
        {
            await context.Category.AddAsync(category);
            await SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            Category category = await context.Category.FindAsync(id);
            if (category != null)
            {
                context.Category.Remove(category);
                await SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Category.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await context.Category.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
