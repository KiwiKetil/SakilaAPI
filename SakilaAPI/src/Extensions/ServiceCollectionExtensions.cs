using System.Text.Json.Serialization;
using SakilaAPI.Middleware;
using SakilaAPI.Repositories;
using SakilaAPI.Repositories.Interfaces;
using SakilaAPI.Services;
using SakilaAPI.Services.Interfaces;
using SakilaAPI.SakilaDbConnection.Interfaces;
using SakilaAPI.SakilaDbConnection;
using SakilaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace SakilaAPI.Extensions;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        // Controllers
        services.AddControllers()
        .AddJsonOptions(opts =>
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())  // enums as string instead of int pt 1.
        );;

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.UseInlineDefinitionsForEnums();  // enums as string instead of int pt 2.
        });

        // ExceptionHandlingMiddleware
        services.AddScoped<MiddlewareExceptionHandler>();

        // Services
        services.AddScoped<IActorService, ActorService>();

        // Mappers are found in WebAppExtensions folder
        
        // Repositories // Change imlementation to use desired ORM, MicroORM
        // services.AddScoped<IActorRepository, ActorRepositoryEF>();
        services.AddScoped<IActorRepository, ActorRepositoryDapper>();

        // MySQLConnector for Dapper
        services.AddScoped<IDbConnectionFactory>(sp =>
        new MySqlConnectionFactory(
        config.GetConnectionString("DefaultConnection")!
        ));

        // DbContext
        services.AddDbContext<SakilaContext>(options =>
            options.UseMySql(
                config.GetConnectionString("DefaultConnection"), // set as env variable name: ConnectionStrings__DefaultConnection
                ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection"))
        ));
        
        return services;
    }
}