using System.Net;
using Microsoft.AspNetCore.Mvc;
using pic2plateApi.Handler;
using pic2plateApi.Model;

namespace pic2plateApi.Controller;

public class RecipeController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly RecipeHandler _recipeHandler;

    public RecipeController(RecipeHandler recipeHandler)
    {
        _recipeHandler = recipeHandler;
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Post([FromBody] RecipeDto recipeDto)
    {
        var recipeId = await _recipeHandler.SaveRecipe(recipeDto);
        return Ok(recipeId);
    }
}