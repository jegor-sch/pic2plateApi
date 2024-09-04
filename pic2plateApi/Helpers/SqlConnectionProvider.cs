using System.Collections.Concurrent;
using System.Data;
using Npgsql;

namespace pic2plateApi.Helpers;

public class SqlConnectionProvider
{
    private const string DefaultConnectionName = "default";
    private const string DefaultPasswordPlaceHolder = "##db_password##";
    private const string DefaultPasswordEnvironmentVariableName = "DbPassword";
    private readonly IConfiguration _configuration;
    private readonly ConcurrentDictionary<string, string> _connectionStrings = new();

    public SqlConnectionProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IDbConnection> GetConnection(string? connectionName = null)
    {
        connectionName ??= DefaultConnectionName;

        var connectionString = _connectionStrings.GetOrAdd(connectionName, GetConnectionString);
        var connection = new NpgsqlConnection(connectionString);

        await connection.OpenAsync();
        return connection;
    }

    private string GetConnectionString(string connectionName)
    {
        var connectionString = _configuration.GetConnectionString(connectionName);

        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentException($"ConnectionString '{connectionName}' was not found.");

        // Replace password with environment variable
        if (!connectionString.Contains(DefaultPasswordPlaceHolder))
            return connectionString;

        var password = Environment.GetEnvironmentVariable(DefaultPasswordEnvironmentVariableName);
        if (password != null)
            return connectionString.Replace(DefaultPasswordPlaceHolder, password);

        return connectionString;
    }
}