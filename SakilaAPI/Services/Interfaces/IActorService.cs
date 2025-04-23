using SakilaAPI.Dtos.Actor;
using SakilaAPI.DTOs.Actor;
using SakilaAPI.Models;
using SakilaAPI.Models.Enums;

namespace SakilaAPI.Services.Interfaces;

public interface IActorService
{
    Task<IEnumerable<ActorDto>> GetActorsAsync(int page, int pageSize);
    Task<ActorDto?>GetActorByIdAsync(ushort id);
    Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByCategoryAsync(FilmCategoryEnum category);
}