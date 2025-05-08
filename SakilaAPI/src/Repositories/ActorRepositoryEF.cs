using Microsoft.EntityFrameworkCore;
using SakilaAPI.Data;
using SakilaAPI.Models;
using SakilaAPI.Repositories.Interfaces;
using SakilaAPI.Dtos.Actor;
using SakilaAPI.Models.Enums;
using SakilaAPI.Helpers;

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

    public async Task<IEnumerable<Actor>> GetActorsAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieveing actors using EF");

        var skipItems = (page -1) * pageSize;

        var res = await _sakilaContext.Actors
            .OrderBy(a => a.ActorId)
            .Skip(skipItems)
            .Take(pageSize)       
            .AsNoTracking()
            .ToListAsync(cancellationToken);      
            return res;
    }

    public async Task<Actor?> GetActorByIdAsync(ushort id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieveing actor by Id using EF");

        return await _sakilaContext.Actors
            .AsNoTracking()
            .SingleOrDefaultAsync(a => a.ActorId == id, cancellationToken);
    }

    public async Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByCategoryAsync(FilmCategoryEnum category, CancellationToken cancellationToken) 
    {
        _logger.LogInformation("Retrieveing actors by film and category using EF");

        var raw = await _sakilaContext.FilmActors
            .AsNoTracking()                                    
            .Where(fa => fa.Film.FilmCategories                
            .Any(fc => fc.CategoryId == (int)category))   
            .Select( fa => new {              
                fa.Actor.FirstName,
                fa.Actor.LastName,
                fa.Film.Title                
            }).ToListAsync(cancellationToken);         

        return raw.Select(x => new ActorFilmCategoryDto(
            StringHelpers.Capitalize(x.FirstName),
            StringHelpers.Capitalize(x.LastName),
            StringHelpers.Capitalize(x.Title),
            category.ToString()           
        ));                
    }

    public async Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByLastNameAsync(string lastName, int page, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieveing actors by film and category using EF");

        var raw = await _sakilaContext.Actors
        .AsNoTracking()
        .Where(a => a.LastName.StartsWith(lastName))
        .SelectMany(a => a.FilmActors, (a, fa) => new { a, fa })
        .SelectMany(t => t.fa.Film.FilmCategories,
                    (t, fc) => new {
                    FirstName  = t.a.FirstName,
                    LastName   = t.a.LastName,
                    Title      = t.fa.Film.Title,
                    CategoryId = fc.CategoryId
                    }).ToListAsync(cancellationToken);

        var dtos = raw
            .AsEnumerable()
            .Select(x => new ActorFilmCategoryDto(
                StringHelpers.Capitalize(x.FirstName),
                StringHelpers.Capitalize(x.LastName),
                StringHelpers.Capitalize(x.Title),
                ((FilmCategoryEnum)x.CategoryId).ToString()
            ))
            .OrderBy(dto => dto.LastName)    
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return dtos;
    }

    public Task<Actor?> DeleteActorAsync(ushort id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Actor?> UpdateActorAsync(ushort id, Actor actor, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Actor?> RegisterActorAsync(Actor actor, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}