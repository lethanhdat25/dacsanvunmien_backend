using dacsanvungmien.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public class WeightRepository : IWeightRepository
    {
        private DacSanVungMienContext context;
        public WeightRepository(DacSanVungMienContext context)
        {
            this.context = context;
        }
        public async Task<Weight> AddWeightAsync(Weight weight)
        {
            await context.Weight.AddAsync(weight);
            await SaveChangesAsync();
            return weight;
        }

        public async Task DeleteWeightAsync(int id)
        {
            Weight weight = await context.Weight.FindAsync(id);
            if (weight != null)
            {
                context.Weight.Remove(weight);
                await SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Weight>> GetWeightsAsync()
        {
            return await context.Weight.ToListAsync();
        }

        public async Task<Weight> GetWeightByIdAsync(int id)
        {
            return await context.Weight.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateWeightAsync(Weight weight)
        {
            context.Entry(weight).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
