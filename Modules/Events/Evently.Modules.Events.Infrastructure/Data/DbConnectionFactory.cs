using System.Data.Common;
using Evently.Modules.Events.Application.Abstractions.Data;
using Microsoft.AspNetCore.Connections;
using Npgsql;

namespace Evently.Modules.Events.Infrastructure.Data;
internal sealed class DbConnectionFactory (NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
      return await dataSource.OpenConnectionAsync();
    }
}
