using Microsoft.EntityFrameworkCore;
using SakilaAPI.Data;
using SakilaAPI.Models;
using SakilaAPI.Repositories.Interfaces;
using SakilaAPI.Dtos.Actor;
using System.Security.Cryptography;
using SakilaAPI.Models.Enums;

namespace SakilaAPI.Repositories;

public class ActorRepositoryEF : IActorRepository
{
    private readonly SakilaContext _sakilaContext;
    private readonly ILogger<ActorRepositoryEF> _logger;

    public ActorRepositoryEF(SakilaContext sakilaContext, ILogger<ActorRepositoryEF> logger)
    {
        _sakilaContext = sakilaContext;
        _logger = logger;
    }  

    public async Task<IEnumerable<Actor>> GetActorsAsync(int page, int pageSize)
    {
        _logger.LogInformation("Retrieveing actors using EF");

        var skipItems = (page -1) * pageSize;

        var res = await _sakilaContext.Actors
        .OrderBy(a => a.ActorId)
        .Skip(skipItems)
        .Take(pageSize)
        // .Distinct()
        .AsNoTracking()
        .ToListAsync();      
        return res;
    }

      public async Task<Actor?> GetActorByIdAsync(ushort id)
    {
        _logger.LogInformation("Retrieveing actor by Id using EF");

        return await _sakilaContext.Actors.FindAsync(id);
    }

    public async Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByCategoryAsync(FilmCategoryEnum category) 
    {
        _logger.LogInformation("Retrieveing actors by film and category using EF");

        return await _sakilaContext.FilmActors
            .AsNoTracking()                                    // read-only, no change-tracking
            .Where(fa => fa.Film.FilmCategories                // navigate into the join
            .Any(fc => fc.CategoryId == (int)category))   // filter by category Id in SQL
            .Select(fa => new ActorFilmCategoryDto(              // project to DTO before materializing
                fa.Actor.FirstName,
                fa.Actor.LastName,
                fa.Film.Title,
                category.ToString()
        ))
        .ToListAsync();                        
    }
}