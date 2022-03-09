using dacsanvungmien.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public class ProvinceRepository : IProvinceRepository
    {
        private DacSanVungMienContext context;
        public ProvinceRepository(DacSanVungMienContext context)
        {
            this.context = context;
        }
/*        public async Task<Province> AddProvinceAsync(Province province)
        {
            await context.Province.AddAsync(province);
            await SaveChangesAsync();
            return province;
        }

        public async Task DeleteProvinceAsync(int id)
        {
            Province province = await context.Province.FindAsync(id);
            if (province != null)
            {
                context.Province.Remove(province);
                await SaveChangesAsync();
            }
        }*/

        public async Task<Province> GetProvinceByIdAsync(int id)
        {
            return await context.Province.FindAsync(id);
        }

        public async Task<IEnumerable<Province>> GetProvincesAsync()
        {
            return await context.Province.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

       /* public async Task UpdateProvinceAsync(Province province)
        {
            context.Entry(province).State = EntityState.Modified;
            await SaveChangesAsync();
        }*/
    }
}
