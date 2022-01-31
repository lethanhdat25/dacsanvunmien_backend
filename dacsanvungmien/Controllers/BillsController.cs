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

        public BillsController(IBillRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<IEnumerable<BillDto>> GetBill()
        {
            return (await repository.GetBillsAsync()).Select(item => item.AsDto());
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
            if (id != bill.Id)
            {
                return BadRequest();
            }
            await repository.UpdateBillAsync(bill);
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
