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
    public class ProvincesController : ControllerBase
    {
        private DacSanVungMienContext context;
        private readonly IProvinceRepository repository;

        public ProvincesController(IProvinceRepository repository, DacSanVungMienContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        // GET: api/Regions
        [HttpGet]
        public async Task<IEnumerable<ProvinceDto>> GetProvince()
        {
            return (await repository.GetProvincesAsync()).Select(item => item.AsDto());
        }

        // GET: api/Regions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetProvince(int id)
        {
            var provinceDb = from province in context.Province
                             where province.RegionId == id
                             select province;
            if (provinceDb == null)
            {
                return NotFound();
            }

            return (new { Province = provinceDb }); ;
        }

        /*// PUT: api/Regions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> PutRegion(int id, Province province)
        {
            if (id != province.Id)
            {
                return BadRequest();
            }
            await repository.UpdateProvinceAsync(province);
            return NoContent();
        }

        // POST: api/Regions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ProvinceDto>> PostProvince(CUProvinceDto provinceDto)
        {
            Province province = new()
            {
                Name = provinceDto.Name,
                RegionId=provinceDto.RegionId
            };
            await repository.AddProvinceAsync(province);
            return CreatedAtAction("GetRegion", new { id = province.Id }, province);
        }

        // DELETE: api/Regions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteProvince(int id)
        {
            var province = await repository.GetProvinceByIdAsync(id);
            if (province == null)
            {
                return NotFound();
            }

            await repository.DeleteProvinceAsync(id);
            return NoContent();
        }*/

    }
}
