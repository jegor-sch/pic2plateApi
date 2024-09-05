using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pic2plateApi.Handler;
using pic2plateApi.Model;

namespace pic2plateApi.Controller;

[ApiController]
[Route("[controller]")]
public class RecipeController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly RecipeHandler _recipeHandler;

    public RecipeController(RecipeHandler recipeHandler)
    {
        _recipeHandler = recipeHandler;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Post([FromBody] RecipeDto recipeDto)
    {
        var recipeId = await _recipeHandler.SaveRecipe(recipeDto);
        return Ok(recipeId);
    }

    [HttpGet("{personId}")]
    [ProducesResponseType(typeof(List<Recipe>), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get([FromRoute] string personId)
    {
        var recipes = await _recipeHandler.Get(personId);
        
        if (recipes is null) return NoContent();
        
        return Ok(recipes);
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _recipeHandler.DeleteRecipe(id);
        return Ok();
    }
}