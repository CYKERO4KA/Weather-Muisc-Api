using Microsoft.AspNetCore.Mvc;
using WeatherMusicBot.Entity;
using WeatherMusicBot.Services;

namespace WeatherMusicBot.Controllers;

[ApiController]
[Route("[controller]")]
public class DbController : ControllerBase
{
    private readonly DbSenderService _senderService;

    public DbController()
    {
        _senderService = new DbSenderService();
    }
    
    [HttpPost]
    public async Task<ActionResult> PostData(User user)
    {
        await _senderService.SendSqlRequest( user );
        
        return Ok();
    }
    
    [HttpPost("CheckUser")]
    public async Task<ActionResult<bool>> CheckUser(User user)
    {
        try
        {
            bool exists = await _senderService.SendAuthorizationRequest(user);
            return Ok(exists);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(User user)
    {
        var result = await _senderService.UpdateUser(user);

        if (result == "User updated successfully.")
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    [HttpGet("GetUserLocation/{chatId}")]
    public async Task<ActionResult<(double Latitude, double Longitude)?>> GetUserLocation(long chatId)
    {
        var location = await _senderService.GetUserLocation(chatId);

        if (location != null)
        {
            return Ok(location);
        }

        return NotFound();
    }


}