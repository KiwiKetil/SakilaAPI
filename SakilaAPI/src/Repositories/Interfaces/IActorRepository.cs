using SakilaAPI.Dtos.Actor;
using SakilaAPI.Models;
using SakilaAPI.Models.Enums;

namespace SakilaAPI.Repositories.Interfaces;

public interface IActorRepository
{
    Task<IEnumerable<Actor>> GetActorsAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<Actor?> GetActorByIdAsync(ushort id, CancellationToken cancellationToken);
    Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByCategoryAsync(FilmCategoryEnum category, CancellationToken cancellationToken);
    Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByLastNameAsync(string lastName, int page, int pageSize, CancellationToken cancellationToken);
    Task<Actor?> DeleteActorAsync(ushort id, CancellationToken ct);
    Task<Actor?> UpdateActorAsync(ushort id, Actor actor, CancellationToken ct);
    Task<Actor?> RegisterActorAsync(Actor actor, CancellationToken ct);
}
