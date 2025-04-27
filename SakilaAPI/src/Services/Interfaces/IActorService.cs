using Microsoft.AspNetCore.Mvc.RazorPages;
using SakilaAPI.Dtos.Actor;
using SakilaAPI.DTOs.Actor;
using SakilaAPI.Models.Enums;

namespace SakilaAPI.Services.Interfaces;

public interface IActorService
{
    Task<IEnumerable<ActorDto>> GetActorsAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<ActorDto?>GetActorByIdAsync(ushort id, CancellationToken cancellationToken);
    Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByCategoryAsync(FilmCategoryEnum category, CancellationToken cancellationToken);
    Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByLastNameAsync(string lastName, CancellationToken cancellationToken, int page, int pageSize);
}