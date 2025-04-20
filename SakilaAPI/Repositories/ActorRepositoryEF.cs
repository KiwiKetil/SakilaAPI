using Microsoft.EntityFrameworkCore;
using SakilaAPI.Data;
using SakilaAPI.Models;
using SakilaAPI.Repositories.Interfaces;

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
}