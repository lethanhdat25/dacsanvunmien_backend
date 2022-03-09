using dacsanvungmien.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public interface IWeightRepository
    {
        Task<IEnumerable<Weight>> GetWeightsAsync();
        Task<Weight> GetWeightByIdAsync(int id);
        Task<Weight> AddWeightAsync(Weight dvt);
        Task DeleteWeightAsync(int id);
        Task UpdateWeightAsync(Weight dvt);
        Task SaveChangesAsync();
    }
}
