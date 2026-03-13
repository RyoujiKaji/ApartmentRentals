using ApartmentRentals.Data.DTOs;
using ApartmentRentals.Data.Models;
using ApartmentRentals.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SpaceStoreApi.Services;
using System.Reflection;

[Route("api/[controller]")]
[ApiController]
public class TenantController : ControllerBase
{
    private readonly IRepository<Tenant> _tenantService;
    private readonly IMapper _mapper;

    public TenantController(IRepository<Tenant> tenantService, IMapper mapper)
    {
        _tenantService = tenantService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<TenantDTO>>> GetAllAsync()
    {
        var t = await _tenantService.GetAllAsync();
        var tDTO = _mapper.Map<IEnumerable<TenantDTO>>(t);
        return tDTO.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tenant>> GetAsync(string id)
    {
        var Tenant = await _tenantService.GetByIdAsync(id);

        if (Tenant == null) return NotFound();

        return Ok(_mapper.Map<TenantDTO>(Tenant));
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredByPropertyAsync([FromQuery] string propertyName, [FromQuery] string value)
    {
        try
        {
            var results = await _tenantService.GetFilteredByPropertyAsync(propertyName, value);
            var dtoResults = _mapper.Map<IEnumerable<TenantDTO>>(results);
            return Ok(dtoResults);
        }
        catch (PropertyFilterException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(TenantNoIdDTO dto)
    {
        var Tenant = _mapper.Map<Tenant>(dto);

        await _tenantService.CreateAsync(Tenant);

        return CreatedAtAction(nameof(GetAsync), new { id = Tenant.Id }, Tenant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, TenantNoIdDTO dto)
    {
        var Tenant = _mapper.Map<Tenant>(dto);

        var success = await _tenantService.UpdateAsync(id, Tenant);

        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _tenantService.DeleteByIdAsync(id);

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
        await _tenantService.DeleteAllAsync();
        return Ok();
    }
}
