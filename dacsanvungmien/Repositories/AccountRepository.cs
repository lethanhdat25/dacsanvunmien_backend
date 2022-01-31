using dacsanvungmien.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private DacSanVungMienContext context;
        public AccountRepository(DacSanVungMienContext context)
        {
            this.context = context;
        }
        public async Task AddUserAsync(Account User)
        {
            await context.Account.AddAsync(User);
            await SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            Account User = await context.Account.FindAsync(id);
            if (User != null)
            {
                context.Account.Remove(User);
                await SaveChangesAsync();
            }
        }

        public async Task<Account> GetUserByEmailAsync(string gmail)
        {
            return await context.Account.FirstOrDefaultAsync(u => u.Gmail == gmail);
        }
        public async Task<Account> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            return await context.Account.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }
        public async Task<Account> GetUserByIdAsync(int id)
        {
            var user = await context.Account.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<Account>> GetUsersAsync()
        {
            return await context.Account.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Account User)
        {
            context.Entry(User).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
