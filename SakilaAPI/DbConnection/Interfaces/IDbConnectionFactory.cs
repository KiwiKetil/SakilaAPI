using System.Data;

namespace SakilaAPI.DbConnection.Interfaces;

public interface IDbConnectionFactory 
{
    Task<IDbConnection> CreateConnectionAsync();
}