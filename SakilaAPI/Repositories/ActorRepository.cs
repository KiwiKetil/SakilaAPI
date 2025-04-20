using Microsoft.EntityFrameworkCore;
using SakilaAPI.Data;
using SakilaAPI.Models;
using SakilaAPI.Repositories.Interfaces;

namespace SakilaAPI.Repositories;

public class ActorRepository : IActorRepository
{
    private readonly SakilaContext _sakilaContext;
    private readonly ILogger<ActorRepository> _logger;

    public ActorRepository(SakilaContext sakilaContext, ILogger<ActorRepository> logger)
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
}