using Dapper;
using pic2plateApi.Helpers;
using pic2plateApi.Model;

namespace pic2plateApi.Repository;

public class RecipeRepository
{
    private readonly SqlConnectionProvider _connectionProvider;

    public RecipeRepository(SqlConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<int> SaveRecipe(int id,RecipeDto recipe)
    {
        const string query = """
                             INSERT INTO recipe(person_id, title, recipe)
                             VALUES (:personId, :title, :recipe)
                             RETURNING id;
                             """;

        var param = new
        {
            id,
            personId = recipe.PersonId,
            title = recipe.Title,
            recipe = recipe.Recipe
        };
        
        using var cn = await _connectionProvider.GetConnection();
        return await cn.QueryFirstOrDefaultAsync<int>(query, param);
    }
}