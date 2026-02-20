using ApartmentRentals.Main.DTOs;
using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentRentals.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceController : ControllerBase
    {
        private readonly IRepository<Space> _repository;
        private readonly IMapper _mapper;
        public SpaceController(IRepository<Space> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] bool full = false)
        {
            var res = await _repository.GetAllAsync();

            if (full)
            {
                return Ok(res.ToList());
            }

            var shortRes = _mapper.Map<IEnumerable<SpaceListDTO>>(res);
            return Ok(shortRes.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Space>> GetAsync(int id)
        {
            var space = await _repository.GetByIdAsync(id);

            if (space == null) return NotFound();

            return space;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync (SpaceCreateDTO s)
        {
            var space = _mapper.Map<Space>(s);

            await _repository.CreateAsync(space);

            return CreatedAtAction(nameof(GetAsync), new { id = space.Id }, space);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync (int id, SpaceCreateDTO s)
        {
            var space = _mapper.Map<Space>(s);
            space.Id = id;

            var res = await _repository.Update(space);

            return res ? NoContent() : NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool res = await _repository.DeleteById(id);

            return res ? NoContent() : NotFound();
        }
    }
}
