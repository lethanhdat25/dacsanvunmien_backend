using dacsanvungmien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public interface IAccountRepository
    {
        Task AddUserAsync(Account User);
        Task DeleteUserAsync(int id);
        Task<Account> GetUserByIdAsync(int id);
        Task<Account> GetUserByEmailAsync(string email);
        Task<Account> GetUserByPhoneNumberAsync(string phoneNumber);
        Task<IEnumerable<Account>> GetUsersAsync();
        Task SaveChangesAsync();
        Task UpdateUserAsync(Account User);
    }
}
