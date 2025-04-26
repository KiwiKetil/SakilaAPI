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
        _logger.LogInformation("Retrieving actors"); 
        var res = await _actorService.GetActorsAsync(page, pageSize, cancellationToken);;
        return Ok(res);
    }

    [HttpGet("{id}", Name = "GetActorById")]
    public async Task<ActionResult<ActorDto>> GetActorById(ushort id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving actor by Id"); 

        var res = await _actorService.GetActorByIdAsync(id, cancellationToken);
        return res != null ? Ok(res) : NotFound("No actor found");
    }

    [HttpGet("categories", Name = "GetActorsFilmAndCategory")]
    public async Task<ActionResult<IEnumerable<ActorFilmCategoryDto>>> GetActorFilmsByCategory([FromQuery]FilmCategoryEnum category,  CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving actors by films and categories"); 
        
        var res = await _actorService.GetActorFilmsByCategoryAsync(category, cancellationToken);
        return res.Any() ? Ok(res) : NotFound("No result found");
    }

    [HttpGet("{lastName}/films", Name = "GetActorFilmsByLastName")]
    public async Task<ActionResult<IEnumerable<ActorFilmCategoryDto>>> GetActorFilmsByLastName(string lastName, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving actor films by lastname");

        var res = await _actorService.GetActorFilmsByLastNameAsync(lastName, cancellationToken);
        return res.Any() ? Ok(res) : NotFound("No result found");
    }
}
