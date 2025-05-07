using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<ActionResult<ActorDto>> GetActorById([FromRoute] ushort id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving actor by Id"); 

        var res = await _actorService.GetActorByIdAsync(id, cancellationToken);
        return res != null ? Ok(res) : NotFound("No actor found");
    }

    [HttpGet("categories", Name = "GetActorsFilmAndCategory")]
    public async Task<ActionResult<IEnumerable<ActorFilmCategoryDto>>> GetActorFilmsByCategory([FromQuery]FilmCategoryEnum category,  CancellationToken cancellationToken) // add pagination
    {
        _logger.LogInformation("Retrieving actors by films and categories"); 
        
        var res = await _actorService.GetActorFilmsByCategoryAsync(category, cancellationToken);
        return res.Any() ? Ok(res) : NotFound("No result found");
    }

    [HttpGet("{lastName}/films", Name = "GetActorFilmsByLastName")]
    public async Task<ActionResult<IEnumerable<ActorFilmCategoryDto>>> GetActorFilmsByLastName([FromRoute] string lastName, CancellationToken cancellationToken, int page = 1, int pageSize = 10)
    {
        _logger.LogInformation("Retrieving actor films by lastname");

        var res = await _actorService.GetActorFilmsByLastNameAsync(lastName, page, pageSize, cancellationToken);
        return res.Any() ? Ok(res) : NotFound("No result found");
    }

    [HttpDelete("{id}", Name = "DeleteActor")]
    public async Task<ActionResult<Actor>> DeleteActor(ushort id, CancellationToken ct) 
    {
        _logger.LogInformation("Deleting actor by id");

        var res = await _actorService.DeleteActorAsync(id, ct);
        return res != null ? Ok(res) : NotFound("Delete unsuccesful - actor not found");
    }

    [HttpPut("{id}", Name = "UpdateActor")]
    public async Task<ActionResult<ActorUpdateDto>> UpdateActor([FromRoute] ushort id, [FromBody] ActorUpdateDto dto, CancellationToken ct) // test uten FROMBODY også, pga ikke har [ApiController]
    {
        _logger.LogInformation("Updating actor by id");

        var res = await _actorService.UpdateActorAsync(id, dto,  ct);
        return res != null ? Ok(res) : NotFound("Update unsuccesful - actor not found");
    }

    [HttpPost("register", Name = "RegisterActor")]
    public async Task<ActionResult<Actor>> RegisterActor([FromBody] ActorRegistrationDto dto, CancellationToken ct)
    {
        _logger.LogInformation("registering new actor");

        var res = await _actorService.RegisterActorAsync(dto, ct);
        return res != null ? Ok(res) : Conflict("Could not register actor. Actor already exist");
    }
}
