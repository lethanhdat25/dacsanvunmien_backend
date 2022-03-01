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
using Microsoft.AspNetCore.Authorization;

namespace dacsanvungmien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository repository;

        public RegionsController(IRegionRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Regions
        [HttpGet]
        public async Task<IEnumerable<RegionDto>> GetRegion()
        {
            return (await repository.GetRegionsAsync()).Select(item => item.AsDto());
        }

        // GET: api/Regions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegionDto>> GetRegion(int id)
        {
            var region = await repository.GetRegionByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return region.AsDto();
        }

        // PUT: api/Regions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> PutRegion(int id, Region region)
        {
            if (id != region.Id)
            {
                return BadRequest();
            }
            await repository.UpdateRegionAsync(region);
            return NoContent();
        }

        // POST: api/Regions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<RegionDto>> PostRegion(CreateRegionDto regionDto)
        {
            Region region = new()
            {
                Name = regionDto.Name
            };
            await repository.AddRegionAsync(region);
            return CreatedAtAction("GetRegion", new { id = region.Id }, region);
        }

        // DELETE: api/Regions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            var region = await repository.GetRegionByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            await repository.DeleteRegionAsync(id);
            return NoContent();
        }

    }
}
