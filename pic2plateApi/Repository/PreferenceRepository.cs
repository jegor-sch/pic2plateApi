using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task<int> Post(string personId, string name)
    {
        const string query = """
                             INSERT INTO preference(person_id, name)
                                    VALUES (:personId, :name)
                                    RETURNING id;
                             """;
        
        using var cn = await _sqlConnectionProvider.GetConnection();

        return await cn.QueryFirstOrDefaultAsync<int>(query, new { personId, name });
    }

    public async Task Delete(string personId)
    {
        const string query = "DELETE FROM preference WHERE person_Id = :personId";
        
        using var cn = await _sqlConnectionProvider.GetConnection();
        
        await cn.ExecuteAsync(query, new { personId });
    }
}