using dacsanvungmien.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public class CartRepository : ICartRepository
    {
        private DacSanVungMienContext context;
        public CartRepository(DacSanVungMienContext context)
        {
            this.context = context;
        }
        public async Task<Cart> AddCartAsync(Cart cart)
        {
            var product = await context.Product.FindAsync(cart.ProductId);
            var bill = await context.Bill.FindAsync(cart.BillId);
            if (product is null || bill is null) return null;
            await context.Bill.AddAsync(bill);
            await SaveChangesAsync();
            return cart;
        }

        public async Task DeleteCartAsync(int id)
        {
            Cart cart = await context.Cart.FindAsync(id);
            if (cart != null)
            {
                context.Cart.Remove(cart);
                await SaveChangesAsync();
            }
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            return await context.Cart.FindAsync(id);
        }

        public async Task<IEnumerable<Cart>> GetCartsAsync()
        {
            return await context.Cart.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            context.Entry(cart).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
