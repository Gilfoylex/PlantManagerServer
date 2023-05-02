using Microsoft.AspNetCore.Mvc;
using PlantManagerServer.Models;
using Shared.Models;

namespace PlantManagerServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantController : ControllerBase
{
    private readonly PlantDbContext _context;
    public PlantController(PlantDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<PlantInfoDisplay>> GetPlantInfo(long id)
    {
        var plant = await _context.PlantEntities.FindAsync(id);
        if (plant == null)
            return NotFound();
        
        return Ok(Helpers.Converts.ConvertFromPlantTableToPlantInfoDisplay(plant));
    }

}