using Dapper;
using pic2plateApi.Helpers;
using pic2plateApi.Model;

namespace pic2plateApi.Repository;

public class PreferenceRepository
{
    public readonly SqlConnectionProvider _sqlConnectionProvider;

    public PreferenceRepository(SqlConnectionProvider sqlConnectionProvider)
    {
        _sqlConnectionProvider = sqlConnectionProvider;
    }

    public async Task<List<Preference>> Get(string personId)
    {
        const string query = """
                             SELECT name FROM preference WHERE deletion_time IS NULL AND person_id = :personId
                             """;

        using var cn = await _sqlConnectionProvider.GetConnection();

        return (await cn.QueryAsync<Preference>(query,new{personId})).ToList();
    }
}