using dacsanvungmien.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public class BillRepository : IBillRepository
    {
        private DacSanVungMienContext context;
        public BillRepository(DacSanVungMienContext context)
        {
            this.context = context;
        }
        public async Task<Bill> AddBillAsync(Bill bill)
        {
            var user = await context.Account.FindAsync(bill.UserId);
            if (user is null) return null;
            await context.Bill.AddAsync(bill);
            await SaveChangesAsync();
            return bill;
        }

        public async Task DeleteBillAsync(int id)
        {
            Bill bill = await context.Bill.FindAsync(id);
            if (bill != null)
            {
                context.Bill.Remove(bill);
                await SaveChangesAsync();
            }
        }

        public async Task<Bill> GetBillByIdAsync(int id)
        {
            return await context.Bill.FindAsync(id);
        }

        public async Task<IEnumerable<Bill>> GetBillsAsync()
        {
            return await context.Bill.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateBillAsync(Bill bill)
        {
            context.Entry(bill).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
