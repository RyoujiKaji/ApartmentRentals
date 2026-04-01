using ApartmentRentals.Data.DTOs;
using ApartmentRentals.Data.Models;
using ApartmentRentals.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceStoreApi.Services;

namespace ApartmentRentals.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceController : ControllerBase
    {

        private readonly IRepository<Space> _spaceService;
        private readonly IMapper _mapper;

        public SpaceController(IRepository<Space> spaceService, IMapper mapper){
             _spaceService = spaceService;
             _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] bool full = false)
        {
            var res = await _spaceService.GetAllAsync();

            if (full)
            {
                return Ok(res.ToList());
            }

            var shortRes = _mapper.Map<IEnumerable<SpaceListDTO>>(res);
            return Ok(shortRes.ToList());
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredByPropertyAsync([FromQuery] string propertyName, [FromQuery] string value)
        {
            try
            {
                var results = await _spaceService.GetFilteredByPropertyAsync(propertyName, value);
                var dtoResults = _mapper.Map<IEnumerable<SpaceDTO>>(results);
                return Ok(dtoResults);
            }
            catch (PropertyFilterException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpaceDTO>> GetAsync(string id)
        {
            var space = await _spaceService.GetByIdAsync(id);

            if (space == null) return NotFound();

            return Ok(space);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync (SpaceNoIdDTO s)
        {
            var space = _mapper.Map<Space>(s);

            await _spaceService.CreateAsync(space);

            return CreatedAtAction(nameof(GetAsync), new { id = space.Id }, space);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync (string id, SpaceNoIdDTO s)
        {
            var space = _mapper.Map<Space>(s);
            space.Id = id;

            var res = await _spaceService.UpdateAsync(id, space);

            return res ? NoContent() : NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool res = await _spaceService.DeleteByIdAsync(id);

            return res ? NoContent() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            /*var tenants = await _tenantService.GetAllAsync();
            foreach (var tenant in tenants)
            {
                await _tenantService.DeleteAsync(tenant.Id);
            }*/
            await _spaceService.DeleteAllAsync();
            return Ok();
        }
    }
}
