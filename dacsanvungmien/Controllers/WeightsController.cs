using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dacsanvungmien.Models;
using dacsanvungmien.Repositories;
using dacsanvungmien.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace dacsanvungmien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightsController : ControllerBase
    {
        private readonly IWeightRepository repository;

        public WeightsController(IWeightRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IEnumerable<WeightDto>> GetWeight()
        {
            return (await repository.GetWeightsAsync()).Select(item => item.AsDto());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeightDto>> GetWeight(int id)
        {
            var weight = await repository.GetWeightByIdAsync(id);

            if (weight == null)
            {
                return NotFound();
            }

            return weight.AsDto();
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PutWeight(int id, Weight weight)
        {
            if (id != weight.Id)
            {
                return BadRequest();
            }
            await repository.UpdateWeightAsync(weight);
            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

        public async Task<ActionResult<WeightDto>> PostWeight(CUWeightDto weightDto)
        {
            Weight weight = new()
            {
                Name = weightDto.Name
            };
            await repository.AddWeightAsync(weight);
            return CreatedAtAction("GetWeight", new { id = weight.Id }, weight);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> DeleteWeight(int id)
        {
            var category = await repository.GetWeightByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await repository.DeleteWeightAsync(id);
            return NoContent();
        }
    }
}
