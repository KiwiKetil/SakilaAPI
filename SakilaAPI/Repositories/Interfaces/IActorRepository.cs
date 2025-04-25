using SakilaAPI.Dtos.Actor;
using SakilaAPI.Models;
using SakilaAPI.Models.Enums;

namespace SakilaAPI.Repositories.Interfaces;

public interface IActorRepository
{
    Task<IEnumerable<Actor>> GetActorsAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<Actor?> GetActorByIdAsync(ushort id, CancellationToken cancellationToken);
    Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByCategoryAsync(FilmCategoryEnum category, CancellationToken cancellationToken);
}