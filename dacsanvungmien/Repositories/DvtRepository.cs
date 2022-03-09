using dacsanvungmien.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public class DvtRepository : IDvtRepository
    {
        private DacSanVungMienContext context;
        public DvtRepository(DacSanVungMienContext context)
        {
            this.context = context;
        }
        public async Task<Dvt> AddDvtAsync(Dvt dvt)
        {
            await context.Dvt.AddAsync(dvt);
            await SaveChangesAsync();
            return dvt;
        }

        public async Task DeleteDvtAsync(int id)
        {
            Dvt dvt = await context.Dvt.FindAsync(id);
            if (dvt != null)
            {
                context.Dvt.Remove(dvt);
                await SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Dvt>> GetDvtsAsync()
        {
            return await context.Dvt.ToListAsync();
        }

        public async Task<Dvt> GetDvtByIdAsync(int id)
        {
            return await context.Dvt.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateDvtAsync(Dvt dvt)
        {
            context.Entry(dvt).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
