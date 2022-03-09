using dacsanvungmien.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public interface IDvtRepository
    {
        Task<IEnumerable<Dvt>> GetDvtsAsync();
        Task<Dvt> GetDvtByIdAsync(int id);
        Task<Dvt> AddDvtAsync(Dvt dvt);
        Task DeleteDvtAsync(int id);
        Task UpdateDvtAsync(Dvt dvt);
        Task SaveChangesAsync();
    }
}
