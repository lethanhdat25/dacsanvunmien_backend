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
    public class BillsController : ControllerBase
    {
        private readonly IBillRepository repository;
        private DacSanVungMienContext context;

        public BillsController(IBillRepository repository, DacSanVungMienContext context)
        {
            this.repository = repository;
            this.context = context;

        }

        // GET: api/Bills
        [HttpGet]
        public IEnumerable<object> GetBill()
        {
            var billData = from bill in context.Bill
                           join user in context.Account on bill.UserId equals user.Id
                           join cart in context.Cart on bill.Id equals cart.BillId
                           join product in context.Product on cart.ProductId equals product.Id
                           select new
                           {
                               id=bill.Id,
                               total = bill.Total,
                               oderTime = bill.OrderTime,
                               status = bill.Status,
                               productName = product.Name,
                               productPrice = product.Price,
                               userName = user.Name,
                               userId=user.Id,
                               phoneNumber = user.PhoneNumber,
                               address = user.UserAddress
                           };
            return billData;
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillDto>> GetBill(int id)
        {
            var bill = await repository.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            return bill.AsDto();
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(int id, Bill bill)
        {
         
            var billInDb = await repository.GetBillByIdAsync(id);
            if (billInDb is null)
            {
                return NotFound();
            }
            billInDb.Status = bill.Status;
            await repository.SaveChangesAsync();
            return NoContent();


        }

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BillDto>> PostBill(BillDto billDto)
        {
            Bill bill = new()
            {
                Status = billDto.Status,
                Total = billDto.Total,
                OrderTime = billDto.OrderTime,
                UserId = billDto.UserId,
            };
            await repository.AddBillAsync(bill);
            return CreatedAtAction("GetBill", new { id = bill.Id }, bill);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            var bill = await repository.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            await repository.DeleteBillAsync(id);
            return NoContent();
        }
    }
}
