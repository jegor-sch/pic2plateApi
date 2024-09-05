using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using pic2plateApi.Handler;
using pic2plateApi.Model;
namespace pic2plateApi.Controller;

[ApiController]
[Route("[controller]")]
public class PreferenceController : ControllerBase
{
    private readonly PreferenceHandler _preferenceHandler;

    public PreferenceController(PreferenceHandler preferenceHandler)
    {
        _preferenceHandler = preferenceHandler;
    }

    [HttpGet("{personId:string}")]
    public async Task<IActionResult> Get(string personId)
        => Ok(await _preferenceHandler.Get(personId));
}