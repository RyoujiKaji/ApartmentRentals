using ApartmentRentals.Data.DTOs;
using ApartmentRentals.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceStoreApi.Services;

[Route("api/[controller]")]
[ApiController]
public class RentalContractController : ControllerBase
{
    //private readonly IRepository<RentalContract> _repository;
    private readonly RentalContractService _rentalService;
    private readonly IMapper _mapper;

    public RentalContractController(RentalContractService rentalService, IMapper mapper)
    {
        //_repository = repository;
        _rentalService = rentalService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<RentalContractDTO>>> GetAllAsync()
    {
        var r = await _rentalService.GetAllAsync();
        var rDTO = _mapper.Map<IEnumerable<RentalContractDTO>>(r);
        return rDTO.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RentalContractDTO>> GetAsync(string id)
    {
        var RentalContract = await _rentalService.GetByIdAsync(id);

        if (RentalContract == null) return NotFound();

        return Ok(RentalContract);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(RentalContractNoIdDTO dto)
    {
        var rentalContract = _mapper.Map<RentalContract>(dto);

        await _rentalService.CreateAsync(rentalContract);

        return CreatedAtAction(nameof(GetAsync), new { id = rentalContract.Id }, rentalContract);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, RentalContractNoIdDTO dto)
    {
        var rentalContract = _mapper.Map<RentalContract>(dto);

        var success = await _rentalService.UpdateAsync(id, rentalContract);

        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _rentalService.DeleteByIdAsync(id);

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
        await _rentalService.DeleteAllAsync();
        return Ok();
    }
}
