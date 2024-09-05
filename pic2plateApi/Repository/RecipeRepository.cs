using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task<int> SaveRecipe(RecipeDto recipe)
    {
        const string query = """
                             INSERT INTO recipe(person_id, title, recipe)
                             VALUES (:personId, :title, :recipe)
                             RETURNING id;
                             """;

        var param = new
        {
            personId = recipe.PersonId,
            title = recipe.Title,
            recipe = recipe.Recipe
        };
        
        using var cn = await _connectionProvider.GetConnection();
        return await cn.QueryFirstOrDefaultAsync<int>(query, param);
    }
    
    public async Task<List<Recipe>?> Get(string personId)
    {
        const string query = """
                             SELECT id, title, recipe
                             FROM recipe
                             WHERE person_id = :personId
                             AND deletion_time IS NULL;
                             """;

        using var cn = await _connectionProvider.GetConnection();
        var recipes = await cn.QueryAsync<Recipe>(query, new { personId });
    
        var recipeList = recipes.ToList();
        return recipeList.Count > 0 ? recipeList : null;
    }

    public async Task Delete(int id)
    {
        const string query = """
                             UPDATE recipe
                             SET deletion_time = NOW()
                             WHERE id = :id;
                             """;
        
        using var cn = await _connectionProvider.GetConnection();
        await cn.ExecuteAsync(query, new { id });
    }
}