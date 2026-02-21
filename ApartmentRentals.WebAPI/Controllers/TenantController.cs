using ApartmentRentals.Main.DTOs;
using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TenantController : ControllerBase
{
    private readonly IRepository<Tenant> _repository;
    private readonly IMapper _mapper;

    public TenantController(IRepository<Tenant> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Tenant>>> GetAllAsync()
    {
        var t = await _repository.GetAllAsync();
        return t.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tenant>> GetAsync(int id)
    {
        var Tenant = await _repository.GetByIdAsync(id);

        if (Tenant == null) return NotFound();

        return Ok(Tenant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(TenantDTO dto)
    {
        var Tenant = _mapper.Map<Tenant>(dto);

        await _repository.CreateAsync(Tenant);

        return CreatedAtAction(nameof(GetAsync), new { id = Tenant.Id }, Tenant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, TenantDTO dto)
    {
        var Tenant = _mapper.Map<Tenant>(dto);
        Tenant.Id = id;

        var success = await _repository.UpdateAsync(Tenant);

        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _repository.DeleteByIdAsync(id);

        return success ? NoContent() : NotFound();
    }
}
