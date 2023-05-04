using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using PlantManagerServer.Models;
using Shared.Models;

namespace PlantManagerServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantController : ControllerBase
{
    private readonly PlantDbContext _context;
    private readonly ILogger<PlantController> _logger;
    public PlantController(PlantDbContext context, ILogger<PlantController> logger)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<PlantInfoDisplay>> GetPlantInfo(long id)
    {
        // 获取客户端IP地址
        var clientIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
        // 获取User-Agent
        //var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

        // 获取请求URL
        var requestUrl = HttpContext.Request.GetDisplayUrl();
        _logger.LogInformation("client address={ClientIpAddress} request, {RequestUrl}", clientIpAddress, requestUrl);
        
        try
        {
            var plant = await _context.PlantEntities.FindAsync(id);
            if (plant == null)
            {
                _logger.LogWarning("client address={ClientIpAddress} request not found, {RequestUrl}", clientIpAddress, requestUrl);
                return NotFound();
            }
        
            return Ok(Helpers.Converts.ConvertFromPlantTableToPlantInfoDisplay(plant));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "client address={ClientIpAddress} request error, {RequestUrl}", clientIpAddress, requestUrl);
            return BadRequest();
        }
    }
}