using ApartmentRentals.Main.DTOs;
using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceStoreApi.Services;

[Route("api/[controller]")]
[ApiController]
public class TenantController : ControllerBase
{
    //private readonly IRepository<Tenant> _repository;
    private readonly TenantService _tenantService;
    private readonly IMapper _mapper;

    public TenantController(TenantService tenantService, IMapper mapper)
    {
        //_repository = repository;
        _tenantService = tenantService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Tenant>>> GetAllAsync()
    {
        var t = await _tenantService.GetAllAsync();
        return t.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tenant>> GetAsync(string id)
    {
        var Tenant = await _tenantService.GetByIdAsync(id);

        if (Tenant == null) return NotFound();

        return Ok(Tenant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(TenantDTO dto)
    {
        var Tenant = _mapper.Map<Tenant>(dto);

        await _tenantService.CreateAsync(Tenant);

        return CreatedAtAction(nameof(GetAsync), new { id = Tenant.Id }, Tenant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, TenantDTO dto)
    {
        var Tenant = _mapper.Map<Tenant>(dto);
        Tenant.Id = id;

        var success = await _tenantService.UpdateAsync(id, Tenant);

        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _tenantService.Delete(id);

        return success ? NoContent() : NotFound();
    }

    [HttpDelete] 
    public async Task<IActionResult> DeleteAll()
    {
        /*var tenants = await _tenantService.GetAllAsync();
        foreach (var tenant in tenants)
        {
            await _tenantService.DeleteAsync(tenant.Id);
        }*/
        await _tenantService.DeleteAll();
        return Ok();
    }
}
