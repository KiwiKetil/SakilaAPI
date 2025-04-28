using System.Data.Common;
using MySqlConnector;
using SakilaAPI.SakilaDbConnection.Interfaces;

namespace SakilaAPI.SakilaDbConnection;

public class MySqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public MySqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<DbConnection> CreateConnectionAsync()
    {
        var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        return connection;
    }
}