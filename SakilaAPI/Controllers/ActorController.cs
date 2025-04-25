using Microsoft.AspNetCore.Mvc;
using SakilaAPI.Dtos.Actor;
using SakilaAPI.DTOs.Actor;
using SakilaAPI.Models;
using SakilaAPI.Models.Enums;
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
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors(CancellationToken cancellationToken, int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Retrieving Actors"); 
        var res = await _actorService.GetActorsAsync(page, pageSize, cancellationToken);;
        return Ok(res);
    }

    [HttpGet("{id}", Name = "GetActorById")]
    public async Task<ActionResult<ActorDto>> GetActorById(ushort id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving Actor by Id"); 

        var res = await _actorService.GetActorByIdAsync(id, cancellationToken);
        return res != null ? Ok(res) : NotFound("No Actor found");
    }

    [HttpGet("films‑and‑categories", Name = "GetActorsFilmAndCategory")]
    public async Task<ActionResult<IEnumerable<ActorFilmCategoryDto>>> GetActorFilmsByCategory([FromQuery]FilmCategoryEnum category,  CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving Actor by Films and categories"); 
        
        return Ok(await _actorService.GetActorFilmsByCategoryAsync(category, cancellationToken));
    }

    [HttpGet("by-actor-lastname", Name = "GetAllActorsFilmsByLastName")]
    public async Task<ActionResult<IEnumerable<
}
