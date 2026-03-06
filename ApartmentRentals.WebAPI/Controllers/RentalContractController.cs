using ApartmentRentals.Main.DTOs;
using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;
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
    public async Task<ActionResult<List<RentalContract>>> GetAllAsync()
    {
        var r = await _rentalService.GetAllAsync();
        return r.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RentalContract>> GetAsync(string id)
    {
        var RentalContract = await _rentalService.GetByIdAsync(id);

        if (RentalContract == null) return NotFound();

        return Ok(RentalContract);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(RentalContractDTO dto)
    {
        var RentalContract = _mapper.Map<RentalContract>(dto);

        await _rentalService.CreateAsync(RentalContract);

        return CreatedAtAction(nameof(GetAsync), new { id = RentalContract.Id }, RentalContract);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, RentalContractDTO dto)
    {
        var RentalContract = _mapper.Map<RentalContract>(dto);
        RentalContract.Id = id;

        var success = await _rentalService.UpdateAsync(id, RentalContract);

        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _rentalService.Delete(id);

        return success ? NoContent() : NotFound();
    }
}
