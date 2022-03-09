using dacsanvungmien.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private DacSanVungMienContext context;
        public RegionRepository(DacSanVungMienContext context)
        {
            this.context = context;
        }
        /*public async Task<Region> AddRegionAsync(Region region)
        {
            await context.Region.AddAsync(region);
            await SaveChangesAsync();
            return region;
        }

        public async Task DeleteRegionAsync(int id)
        {
            Region region = await context.Region.FindAsync(id);
            if (region != null)
            {
                context.Region.Remove(region);
                await SaveChangesAsync();
            }
        }*/

        public async Task<Region> GetRegionByIdAsync(int id)
        {
            return await context.Region.FindAsync(id);
        }

        public async Task<IEnumerable<Region>> GetRegionsAsync()
        {
            return await context.Region.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

       /* public async Task UpdateRegionAsync(Region region)
        {
            context.Entry(region).State = EntityState.Modified;
            await SaveChangesAsync();
        }*/
    }
}
