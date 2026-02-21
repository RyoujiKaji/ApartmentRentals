using ApartmentRentals.Main.DTOs;
using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RentalContractController : ControllerBase
{
    private readonly IRepository<RentalContract> _repository;
    private readonly IMapper _mapper;

    public RentalContractController(IRepository<RentalContract> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<RentalContract>>> GetAllAsync()
    {
        var r = await _repository.GetAllAsync();
        return r.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RentalContract>> GetAsync(int id)
    {
        var RentalContract = await _repository.GetByIdAsync(id);

        if (RentalContract == null) return NotFound();

        return Ok(RentalContract);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(TenantDTO dto)
    {
        var RentalContract = _mapper.Map<RentalContract>(dto);

        await _repository.CreateAsync(RentalContract);

        return CreatedAtAction(nameof(GetAsync), new { id = RentalContract.Id }, RentalContract);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, TenantDTO dto)
    {
        var RentalContract = _mapper.Map<RentalContract>(dto);
        RentalContract.Id = id;

        var success = await _repository.UpdateAsync(RentalContract);

        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _repository.DeleteByIdAsync(id);

        return success ? NoContent() : NotFound();
    }
}
