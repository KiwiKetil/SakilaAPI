using Microsoft.AspNetCore.Mvc;
using SakilaAPI.DTOs.Actor;
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
        _logger.LogInformation("Retrieving Actors"); 
        var res = await _actorService.GetActorsAsync(page, pageSize);;
        return Ok(res);
    }

    [HttpGet("{id}", Name = "GetActorById")]
    public async Task<ActionResult<ActorDto>> GetActorById(int id)
    {
         _logger.LogInformation("Retrieving Actor by Id"); 

         var res = await _actorService.GetActorByIdAsync(id);
         return res != null ? Ok(res) : NotFound("No Actor found");
    }
}
