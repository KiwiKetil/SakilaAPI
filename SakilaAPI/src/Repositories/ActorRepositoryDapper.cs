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
        var skipNumber = (page -1) * pageSize;        
        parameters.Add("PageSize", pageSize);
        parameters.Add("Skipnumber", skipNumber);

        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = @"
                    SELECT 
                        actor_id AS ActorId,
                        CONCAT(
                            UPPER(SUBSTRING(first_name, 1, 1)),
                            LOWER(SUBSTRING(first_name, 2))
                           ) AS FirstName,
                        CONCAT(
                            UPPER(SUBSTRING(last_name, 1, 1)),
                            LOWER(SUBSTRING(last_name, 2))
                           ) AS LastName,
                        last_update AS LastUpdate
                    FROM Actor
                    ORDER BY actor_id
                    LIMIT @PageSize
                    OFFSET @SkipNumber
                    ";

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
                        CONCAT(
                            UPPER(SUBSTRING(first_name, 1, 1)),
                            LOWER(SUBSTRING(first_name, 2))
                           ) AS FirstName,
                        CONCAT(
                            UPPER(SUBSTRING(last_name, 1, 1)),
                            LOWER(SUBSTRING(last_name, 2))
                           ) AS LastName,
                        last_update AS LastUpdate
                    FROM Actor
                    WHERE actor_id = @Id
                    ";

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
                        CONCAT(
                            UPPER(SUBSTRING(a.first_name, 1, 1)),
                            LOWER(SUBSTRING(a.first_name, 2))
                           ) AS FirstName,
                        CONCAT(
                            UPPER(SUBSTRING(a.last_name, 1, 1)),
                            LOWER(SUBSTRING(a.last_name, 2))
                           ) AS LastName,
                        CONCAT(
                           UPPER(SUBSTRING(f.title, 1, 1)),
                            LOWER(SUBSTRING(f.title, 2))
                           ) AS Film,                      
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
    
    public async Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByLastNameAsync(string lastName, CancellationToken cancellationToken, int page, int pageSize)
    {
        _logger.LogInformation("Retrieveing actors by film and category using Dapper");

        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
   
        var sql = @"
                    SELECT 
                        CONCAT(
                            UPPER(SUBSTRING(a.first_name, 1, 1)),
                            LOWER(SUBSTRING(a.first_name, 2))
                           ) AS FirstName,
                        CONCAT(
                            UPPER(SUBSTRING(a.last_name, 1, 1)),
                            LOWER(SUBSTRING(a.last_name, 2))
                           ) AS LastName,
                        CONCAT(
                            UPPER(SUBSTRING(f.title, 1, 1)),
                            LOWER (SUBSTRING(f.title, 2))
                            ) AS Film,                           
                        c.name AS Category
                    FROM film_actor fa
                    JOIN actor a ON fa.actor_id = a.actor_id
                    JOIN film f ON fa.film_id = f.film_id
                    JOIN film_category fc ON f.film_id = fc.film_id
                    JOIN category c ON fc.category_id = c.category_id
                    WHERE a.last_name LIKE CONCAT(@LastName, '%')   
                    ORDER BY a.last_name
                    LIMIT @PageSize
                    OFFSET @SkipNumber       
                    ";

        var skipNumber = (page -1) * pageSize;     
        var parameters = new 
        {
            PageSize = pageSize,
            SkipNumber = skipNumber,
            LastName = lastName
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
    
    // Update 

    // create

    // delete
}