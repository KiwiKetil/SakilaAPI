using Dapper;
using SakilaAPI.DbConnection.Interfaces;
using SakilaAPI.Models;
using SakilaAPI.Repositories.Interfaces;

namespace SakilaAPI.Repositories;

public class ActorRepositoryDapper : IActorRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly ILogger<ActorRepositoryDapper> _logger;

    public ActorRepositoryDapper(IDbConnectionFactory dbConnectionFactory, ILogger<ActorRepositoryDapper> logger)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _logger = logger;
    }   

    public async Task<IEnumerable<Actor>> GetActorsAsync(int page, int pageSize)
    {
        _logger.LogInformation("Retrieving actors using Dapper");

        var parameters = new DynamicParameters();

        var skipnumber = (page -1) * pageSize;        
        parameters.Add("PageSize", pageSize);
        parameters.Add("Skipnumber", skipnumber);

        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = @"
                    SELECT 
                        actor_id   AS ActorId,
                        first_name AS FirstName,
                        last_name  AS LastName,
                        last_update AS LastUpdate
                    FROM Actor
                    ORDER BY actor_id
                    LIMIT @PageSize
                    OFFSET @Skipnumber";


        var actors = await connection.QueryAsync<Actor>(sql, parameters);

        return actors;
    }

     public async Task<Actor?> GetActorByIdAsync(ushort id)
    {
        _logger.LogInformation("Retrieving actor by Id using Dapper");

        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = @"
                    SELECT
                     actor_id   AS ActorId,
                        first_name AS FirstName,
                        last_name  AS LastName,
                        last_update AS LastUpdate
                    FROM Actor
                    WHERE actor_id = @Id";

        var actor = await connection.QueryFirstOrDefaultAsync<Actor>(sql, new { Id = id});
        return actor;
    }
}