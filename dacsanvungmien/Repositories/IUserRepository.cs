using dacsanvungmien.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Account>> GetUsersAsync();
        Task<Account> GetUserByIdAsync(int id);
        Task DeleteUserAsync(int id);
        Task UpdateUserAsync(Account user);
        Task SaveChangesAsync();
        Task<Account> GetUserByEmailAsync(string email);

    }
}
