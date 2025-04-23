using SakilaAPI.Dtos.Actor;
using SakilaAPI.Models;

namespace SakilaAPI.Repositories.Interfaces;

public interface IActorRepository
{
    Task<IEnumerable<Actor>> GetActorsAsync(int page, int pageSize);
    Task<Actor?> GetActorByIdAsync(ushort id);
    Task<IEnumerable<ActorFilmCategoryDto>> GetActorsFilmAndCategoryAsync();
}
