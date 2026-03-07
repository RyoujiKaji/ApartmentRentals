using ApartmentRentals.Data.DTOs;
using ApartmentRentals.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceStoreApi.Services;

[Route("api/[controller]")]
[ApiController]
public class LandlordController : ControllerBase
{
    //private readonly IRepository<Landlord> _repository;
    private readonly LandlordService _landlordService;
    private readonly IMapper _mapper;

    public LandlordController(LandlordService landlordService, IMapper mapper)
    {
        //_repository = repository;
        _landlordService = landlordService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<LandlordDTO>>> GetAllAsync()
    {
        var l = await _landlordService.GetAllAsync();
        var lDTO = _mapper.Map<IEnumerable<LandlordDTO>>(l);
        return lDTO.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LandlordDTO>> GetAsync(string id)
    {
        var landlord = await _landlordService.GetByIdAsync(id);

        if (landlord == null) return NotFound();

        return Ok(landlord);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(LandlordNoIdDTO dto)
    {
        var landlord = _mapper.Map<Landlord>(dto);

        await _landlordService.CreateAsync(landlord);

        return CreatedAtAction(nameof(GetAsync), new { id = landlord.Id }, landlord);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, LandlordNoIdDTO dto)
    {
        var landlord = _mapper.Map<Landlord>(dto);
        landlord.Id = id;

        var success = await _landlordService.UpdateAsync(id, landlord);

        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _landlordService.DeleteByIdAsync(id);

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
        await _landlordService.DeleteAllAsync();
        return Ok();
    }
}
