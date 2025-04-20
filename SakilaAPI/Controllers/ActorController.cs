using Microsoft.AspNetCore.Mvc;
using SakilaAPI.Models;
using SakilaAPI.Services.Interfaces;

namespace SakilaAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ActorController : ControllerBase
{
    private readonly IActorService _actorService;
    private readonly ILogger<ActorController> _logger; 
   
    public ActorController(IActorService actorService, ILogger<ActorController> logger)
    {
        _actorService = actorService;
        _logger = logger;
    }

    [HttpGet(Name = "GetActors")]
    public async Task<ActionResult<IEnumerable<Actor>>> GetActors(int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Getting Actors"); 
        var res = await _actorService.GetActorsAsync(page, pageSize);;
        return Ok(res);
    }
}
