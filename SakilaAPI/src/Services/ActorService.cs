using SakilaAPI.Dtos.Actor;
using SakilaAPI.DTOs.Actor;
using SakilaAPI.Mappers.Interfaces;
using SakilaAPI.Models;
using SakilaAPI.Models.Enums;
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

    public async Task<IEnumerable<ActorDto>> GetActorsAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var actors = await _actorRepository.GetActorsAsync(page, pageSize, cancellationToken);       

        var dtos = actors.Select(actor => 
        {
            var dto = _actorMapper.MapToDto(actor);
            return dto;
        }).ToList();
        
        return dtos;
    }

    public async Task<ActorDto?> GetActorByIdAsync(ushort id, CancellationToken cancellationToken)
    {
        var actor = await _actorRepository.GetActorByIdAsync(id, cancellationToken);
        return actor != null ? _actorMapper.MapToDto(actor) : null;;
    }

    public async Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByCategoryAsync(FilmCategoryEnum category, CancellationToken cancellationToken)
    {
        return await _actorRepository.GetActorFilmsByCategoryAsync(category, cancellationToken);
    }

    public async Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByLastNameAsync(string lastName, int page, int pageSize, CancellationToken cancellationToken)
    {
        return await _actorRepository.GetActorFilmsByLastNameAsync(lastName, page, pageSize, cancellationToken);
    } 

    public async Task<ActorDto?> DeleteActorAsync(ushort id, CancellationToken ct)
    {
        var res = await _actorRepository.DeleteActorAsync(id, ct);
        return res != null ? _actorMapper.MapToDto(res): null;        
    }

    public async Task<ActorDto?> UpdateActorAsync(ushort id, ActorUpdateDto dto, CancellationToken ct)
    {
        var res =  await _actorRepository.UpdateActorAsync(id, dto, ct); 
        return res != null ? _actorMapper.MapToDto(res) : null;
    }
}