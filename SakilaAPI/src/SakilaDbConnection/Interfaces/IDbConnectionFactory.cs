using System.Data;
using System.Data.Common;

namespace SakilaAPI.SakilaDbConnection.Interfaces;

public interface IDbConnectionFactory
{
    Task<DbConnection> CreateConnectionAsync();
}
