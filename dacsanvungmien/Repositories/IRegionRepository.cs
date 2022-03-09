using dacsanvungmien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetRegionsAsync();
        Task<Region> GetRegionByIdAsync(int id);
        /*Task<Region> AddRegionAsync(Region region);
        Task DeleteRegionAsync(int id);
        Task UpdateRegionAsync(Region region);*/
        Task SaveChangesAsync();
    }
}
