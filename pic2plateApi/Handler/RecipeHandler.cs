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

    public async Task<int> SaveRecipe(int id ,RecipeDto recipe)
    {
        var createdRecipeId = await _recipeRepo.SaveRecipe(id,recipe);

        return createdRecipeId;
    }
}