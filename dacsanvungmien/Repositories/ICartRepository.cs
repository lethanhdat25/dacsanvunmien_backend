using dacsanvungmien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetCartsAsync();
        Task<Cart> GetCartByIdAsync(int id);
        Task<Cart> AddCartAsync(Cart cart);
        Task DeleteCartAsync(int id);
        Task UpdateCartAsync(Cart cart);
        Task SaveChangesAsync();
    }
}
