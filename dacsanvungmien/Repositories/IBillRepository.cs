using dacsanvungmien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public interface IBillRepository
    {
        Task<IEnumerable<Bill>> GetBillsAsync();
        Task<Bill> GetBillByIdAsync(int id);
        Task<Bill> AddBillAsync(Bill bill);
        Task DeleteBillAsync(int id);
        Task UpdateBillAsync(Bill bill);
        Task SaveChangesAsync();
    }
}
