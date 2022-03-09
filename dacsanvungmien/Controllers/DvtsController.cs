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
    public class DvtsController : ControllerBase
    {
        private readonly IDvtRepository repository;

        public DvtsController(IDvtRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IEnumerable<DvtDto>> GetDvt()
        {
            return (await repository.GetDvtsAsync()).Select(item => item.AsDto());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DvtDto>> GetDvt(int id)
        {
            var dvt = await repository.GetDvtByIdAsync(id);

            if (dvt == null)
            {
                return NotFound();
            }

            return dvt.AsDto();
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PutDvt(int id, Dvt dvt)
        {
            if (id != dvt.Id)
            {
                return BadRequest();
            }
            await repository.UpdateDvtAsync(dvt);
            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

        public async Task<ActionResult<DvtDto>> PostDvt(CUDvtDto dvtDto)
        {
            Dvt dvt = new()
            {
                Name = dvtDto.Name
            };
            await repository.AddDvtAsync(dvt);
            return CreatedAtAction("GetDvt", new { id = dvt.Id }, dvt);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> DeleteDvt(int id)
        {
            var dvt = await repository.GetDvtByIdAsync(id);
            if (dvt == null)
            {
                return NotFound();
            }

            await repository.DeleteDvtAsync(id);
            return NoContent();
        }
    }
}
