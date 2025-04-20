using System.Data;
using SakilaAPI.DbConnection.Interfaces;
using MySqlConnector;

namespace SakilaAPI.DbConnection;

public class MySqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public MySqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        return connection;
    }
}