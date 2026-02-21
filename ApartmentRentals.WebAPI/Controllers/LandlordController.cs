using ApartmentRentals.Main.DTOs;
using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class LandlordController : ControllerBase
{
    private readonly IRepository<Landlord> _repository;
    private readonly IMapper _mapper;

    public LandlordController(IRepository<Landlord> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Landlord>>> GetAllAsync()
    {
        var l = await _repository.GetAllAsync();
        return l.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Landlord>> GetAsync(int id)
    {
        var landlord = await _repository.GetByIdAsync(id);

        if (landlord == null) return NotFound();

        return Ok(landlord);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(LandlordDTO dto)
    {
        var landlord = _mapper.Map<Landlord>(dto);

        await _repository.CreateAsync(landlord);

        return CreatedAtAction(nameof(GetAsync), new { id = landlord.Id }, landlord);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, LandlordDTO dto)
    {
        var landlord = _mapper.Map<Landlord>(dto);
        landlord.Id = id;

        var success = await _repository.UpdateAsync(landlord);

        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _repository.DeleteByIdAsync(id);

        return success ? NoContent() : NotFound();
    }
}
