using dacsanvungmien.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public interface IProvinceRepository
    {
        Task<IEnumerable<Province>> GetProvincesAsync();
        Task<Province> GetProvinceByIdAsync(int id);
       /* Task<Province> AddProvinceAsync(Province province);
        Task DeleteProvinceAsync(int id);
        Task UpdateProvinceAsync(Province province);*/
        Task SaveChangesAsync();
    }
}
