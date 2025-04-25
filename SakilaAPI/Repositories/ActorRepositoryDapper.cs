using Dapper;
using SakilaAPI.DbConnection.Interfaces;
using SakilaAPI.Dtos.Actor;
using SakilaAPI.Models;
using SakilaAPI.Models.Enums;
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

    public async Task<IEnumerable<Actor>> GetActorsAsync(int page, int pageSize, CancellationToken cancellationToken)
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

        var cmd = new CommandDefinition
        (
            commandText: sql,
            parameters : parameters
        );

        var actors = await connection.QueryAsync<Actor>(cmd);

        return actors;
    }

    public async Task<Actor?> GetActorByIdAsync(ushort id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving actor by Id using Dapper");

        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = @"
                    SELECT
                        actor_id AS ActorId,
                        first_name AS FirstName,
                        last_name  AS LastName,
                        last_update AS LastUpdate
                    FROM Actor
                    WHERE actor_id = @Id";

        var cmd = new CommandDefinition
        (
            commandText: sql,
            parameters: new { Id = id },  
            cancellationToken: cancellationToken        
        );

        var actor = await connection.QueryFirstOrDefaultAsync<Actor>(cmd);
        return actor;
    }

    public async Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByCategoryAsync(FilmCategoryEnum category, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieveing actors by film and category using Dapper");

        using var connection = await _dbConnectionFactory.CreateConnectionAsync();      

        var sql = @"
                    SELECT 
                        a.first_name AS Firstname, 
                        a.last_name AS Lastname,
                        f.title AS Film,
                        @CategoryName AS Category
                    FROM film_actor fa
                    JOIN actor a ON fa.actor_id = a.actor_id
                    JOIN film f ON fa.film_id = f.film_id
                    JOIN film_category fc ON f.film_id = fc.film_id
                    WHERE fc.category_id = @CategoryId
                    ";                        

        var parameters = new
        {
            CategoryId   = (int)category,
            CategoryName = category.ToString()
        };

        var cmd = new CommandDefinition
        (
            commandText: sql,
            parameters: parameters,
            cancellationToken: cancellationToken        
        );

        var res = await connection.QueryAsync<ActorFilmCategoryDto>(cmd);
        return res;
    }

    // another get with join

    

    // Update 

    // create

    // delete
}