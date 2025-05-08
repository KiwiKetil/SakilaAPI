using Dapper;
using SakilaAPI.Dtos.Actor;
using SakilaAPI.Models;
using SakilaAPI.Models.Enums;
using SakilaAPI.Repositories.Interfaces;
using SakilaAPI.DB.Interfaces;
using SakilaAPI.DTOs.Actor;
using System.Runtime.CompilerServices;
using System.Globalization;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

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

        await using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        const string sql = @"
            SELECT 
                actor_id AS ActorId,
                first_name AS FirstName,
                last_name AS LastName,
                last_update AS LastUpdate
            FROM 
                Actor
            ORDER BY
                actor_id
            LIMIT @PageSize
            OFFSET @SkipNumber
            ";

        var skipNumber = (page -1) * pageSize; 

        var parameters = new DynamicParameters();          
        parameters.Add("PageSize", pageSize);
        parameters.Add("Skipnumber", skipNumber);

        var cmd = new CommandDefinition(
            commandText: sql,
            parameters : parameters,
            cancellationToken : cancellationToken            
        );

        var actors = await connection.QueryAsync<Actor>(cmd);

        return actors;
    }

    public async Task<Actor?> GetActorByIdAsync(ushort id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving actor by Id using Dapper");

        await using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        const string sql = @"
            SELECT
                actor_id AS ActorId,
                first_name AS FirstName,
                last_name AS LastName,
                last_update AS LastUpdate
            FROM 
                Actor
            WHERE
                actor_id = @Id
                ";

        var cmd = new CommandDefinition(
            commandText: sql,
            parameters: new { Id = id },  
            cancellationToken: cancellationToken        
        );

        var actor = await connection.QuerySingleOrDefaultAsync<Actor>(cmd);
        return actor;
    }
    
    public async Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByCategoryAsync(FilmCategoryEnum category, CancellationToken cancellationToken) // add pagination
    {
        _logger.LogInformation("Retrieveing actors by film and category using Dapper");

        await using var connection = await _dbConnectionFactory.CreateConnectionAsync();      
                                   
        const string sql = @"
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
                    ) AS FilmTitle,                      
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

        var cmd = new CommandDefinition(
            commandText: sql,
            parameters: parameters,
            cancellationToken: cancellationToken        
        );

        var res = await connection.QueryAsync<ActorFilmCategoryDto>(cmd);
        return res;
    }
    
    public async Task<IEnumerable<ActorFilmCategoryDto>> GetActorFilmsByLastNameAsync(string lastName, int page, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieveing actors by film and category using Dapper");

        await using var connection = await _dbConnectionFactory.CreateConnectionAsync();
   
        const string sql = @"
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
                    ) AS FilmTitle,                           
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
                 
        var cmd = new CommandDefinition(
            commandText: sql,
            parameters: parameters,
            cancellationToken: cancellationToken
        );      
    
       var res = await connection.QueryAsync<ActorFilmCategoryDto>(cmd);
       return res;
    }

    public async Task<Actor?> DeleteActorAsync(ushort id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting actor using Dapper");

        await using var connection  = await _dbConnectionFactory.CreateConnectionAsync();
        await using var transaction = await connection.BeginTransactionAsync(ct);

        try
        {                               
            const string selectSql = @"
                SELECT
                        actor_id AS ActorId,
                        first_name AS FirstName,
                        last_name AS LastName,
                        last_update AS LastUpdate
                    FROM 
                        actor
                    WHERE
                        actor_id = @Id;
                ";

                var idParameter = new { Id = id};

                var actor = await connection.QuerySingleOrDefaultAsync<Actor>(new CommandDefinition(
                    selectSql,
                    idParameter,
                    transaction,
                    cancellationToken : ct
                )); 

                if (actor is null)
                {
                    await transaction.RollbackAsync(ct);
                    return null;
                }               

                const string deleteFilmActorSql = @"
                DELETE
                    FROM film_actor        
                WHERE 
                    actor_id = @Id;               
                ";

                await connection.ExecuteAsync(new CommandDefinition( 
                    deleteFilmActorSql,
                    idParameter,
                    transaction,
                    cancellationToken : ct
                ));               

                const string deleteActorSql = @"
                DELETE
                    FROM actor
                WHERE
                    actor_id = @Id;
                ";    

                var success = await connection.ExecuteAsync(new CommandDefinition(
                    deleteActorSql,
                    idParameter,
                    transaction,
                    cancellationToken : ct
                ));                   
           
                if (success != 1)
                {
                    await transaction.RollbackAsync(ct);
                    return null;
                }                        

            await transaction.CommitAsync(ct);
            return actor;
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }
  
    public async Task<Actor?> UpdateActorAsync(ushort id, Actor actor, CancellationToken ct = default)
    {
        _logger.LogInformation("Updating actor using Dapper");

        await using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        await using var transaction = await connection.BeginTransactionAsync(ct);

        try
        {                                          
            const string updateSql = @"
                UPDATE actor
                SET 
                    first_name = @FirstName,
                    last_name = @LastName
                WHERE actor_id = @ActorId";
       
            var affected = await connection.ExecuteAsync( new CommandDefinition(
                updateSql,
                new { actor.FirstName, actor.LastName, ActorId = id },
                transaction: transaction,
                cancellationToken:ct
            ));           
            
            if(affected != 1)
            {
                _logger.LogWarning("UpdateActorAsync({ActorId}) affected {Rows} rows", id, affected);
                await transaction.RollbackAsync(ct);
                return null;
            }             
         
            const string selectSql = @"
                SELECT
                    actor_id AS ActorId,
                    first_name AS FirstName,
                    last_name  AS LastName
                FROM
                    actor
                WHERE
                     actor_id = @ActorId
                ";              

            var updatedActor = await connection
                .QuerySingleOrDefaultAsync<Actor>(new CommandDefinition(
                    selectSql,
                    new { ActorId = id },
                    transaction: transaction,
                    cancellationToken:ct 
                ));                     

            await transaction.CommitAsync(ct);           
            return updatedActor;
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }    

    public async Task<Actor?> RegisterActorAsync(Actor actor, CancellationToken ct)     // husk validering dto
    {
        _logger.LogInformation("Registering actor using Dapper"); 

        await using var connection = await _dbConnectionFactory.CreateConnectionAsync();      
        
        const string registerSql = @"
            INSERT INTO 
                Actor (first_name, last_name)
            VALUES
                (@FirstName, @LastName);          
        ";

        var parameters = new { actor.FirstName, actor.LastName};

        await connection.ExecuteScalarAsync<int>(new CommandDefinition(registerSql, parameters : parameters, cancellationToken : ct));                   

        const string lastIdSql = @"
            SELECT LAST_INSERT_ID()
        ";

        var newId = await connection.ExecuteScalarAsync<int>( new CommandDefinition(lastIdSql, cancellationToken : ct));       
       
        const string selectSql = @"
            SELECT
                actor_id AS ActorId,
                first_name AS FirstName,
                last_name AS LastName,
                last_update AS LastUpdate
            FROM
                Actor
            WHERE 
                actor_id = @Id
        ";

        var registeredActor = await connection.QuerySingleOrDefaultAsync<Actor>(new CommandDefinition(selectSql, parameters : new  {Id = newId}, cancellationToken : ct));
        return registeredActor;
    }    
}
