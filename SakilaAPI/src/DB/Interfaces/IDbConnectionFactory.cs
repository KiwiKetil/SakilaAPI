using System.Data;
using System.Data.Common;

namespace SakilaAPI.DB.Interfaces;

public interface IDbConnectionFactory
{
    Task<DbConnection> CreateConnectionAsync();
}
