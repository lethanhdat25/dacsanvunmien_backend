using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dacsanvungmien.Dtos;
using dacsanvungmien.Models;
using dacsanvungmien.Repositories;

namespace dacsanvungmien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository repository;

        public CartsController(ICartRepository repository)
        {
            this.repository = repository;
        }
        // GET: api/Carts
        [HttpGet]
        public async Task<IEnumerable<CartDto>> GetCart()
        {
            return (await repository.GetCartsAsync()).Select(item => item.AsDto());
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetCart(int id)
        {
            var cart = await repository.GetCartByIdAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart.AsDto();
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartDto(int id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }
            await repository.UpdateCartAsync(cart);
            return NoContent();
        }

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CartDto>> PostCartDto(CartDto cartDto)
        {
            Cart cart = new()
            {
                Amount = cartDto.Amount,
                BillId = cartDto.BillId,
                ProductId = cartDto.ProductId,
                Total = cartDto.Total,
            };
            await repository.AddCartAsync(cart);
            return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartDto(int id)
        {
            var cart = await repository.GetCartByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            await repository.DeleteCartAsync(id);
            return NoContent();
        }
    }
}
