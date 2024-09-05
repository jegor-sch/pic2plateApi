using System.Collections.Generic;
using System.Threading.Tasks;
using pic2plateApi.Model;
using pic2plateApi.Repository;

namespace pic2plateApi.Handler;

public class RecipeHandler
{
    private readonly RecipeRepository _recipeRepo;

    public RecipeHandler(RecipeRepository recipeRepo)
    {
        _recipeRepo = recipeRepo;
    }

    public async Task<int> SaveRecipe(RecipeDto recipe)
    {
        var createdRecipeId = await _recipeRepo.SaveRecipe(recipe);

        return createdRecipeId;
    }

    public async Task<List<Recipe>?> Get(string personId)
    {
        var recipes = await _recipeRepo.Get(personId);

        return recipes;
    }

    public async Task DeleteRecipe(int id)
    {
        await _recipeRepo.Delete(id);
    }
}