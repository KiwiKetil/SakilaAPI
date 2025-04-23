using SakilaAPI.Dtos.Actor;
using SakilaAPI.DTOs.Actor;
using SakilaAPI.Mappers;
using SakilaAPI.Mappers.Interfaces;
using SakilaAPI.Models;
using SakilaAPI.Repositories.Interfaces;
using SakilaAPI.Services.Interfaces;

namespace SakilaAPI.Services;

public class ActorService : IActorService
{
    private readonly IActorRepository _actorRepository;
    private readonly IMapper<Actor, ActorDto> _actorMapper;

    public ActorService(IActorRepository actorRepository, IMapper<Actor, ActorDto> actorMapper)
    {
        _actorRepository = actorRepository;
        _actorMapper = actorMapper;
    } 

    public async Task<IEnumerable<ActorDto>> GetActorsAsync(int page, int pageSize)
    {
        var actors = await _actorRepository.GetActorsAsync(page, pageSize);       

        var dtos = actors.Select(actor => 
        {
            var dto = _actorMapper.MapToDto(actor);
            return dto;
        }).ToList();
        
        return dtos;
    }

    public async Task<ActorDto?> GetActorByIdAsync(ushort id)
    {
        var actor = await _actorRepository.GetActorByIdAsync(id);
        return actor != null ? _actorMapper.MapToDto(actor) : null;;
    }

    public async Task<IEnumerable<ActorFilmCategoryDto>> GetActorsFilmAndCategoryAsync()
    {
        return await _actorRepository.GetActorsFilmAndCategoryAsync();
    }
}