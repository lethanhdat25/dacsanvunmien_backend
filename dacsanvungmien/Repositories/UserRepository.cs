using dacsanvungmien.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DacSanVungMienContext context;
        public UserRepository(DacSanVungMienContext context)
        {
            this.context = context;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await context.Account.FindAsync(id);
            if (user != null)
            {
                context.Account.Remove(user);
                await SaveChangesAsync();
            }
        }

        public async Task<Account> GetUserByIdAsync(int id)
        {
            return await context.Account.FindAsync(id);
        }

        public async Task<IEnumerable<Account>> GetUsersAsync()
        {
            return await context.Account.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Account user)
        {
            context.Entry(user).State = EntityState.Modified;
            await SaveChangesAsync();
        }
        public async Task<Account> GetUserByEmailAsync(string gmail)
        {
            return await context.Account.FirstOrDefaultAsync(u => u.Gmail == gmail);
        }
    }
}
