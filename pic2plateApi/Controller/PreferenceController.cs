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

    [HttpGet("{personId}")]
    public async Task<IActionResult> Get(string personId)
        => Ok(await _preferenceHandler.Get(personId));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PreferenceDto dto)
    {
        var status = await _preferenceHandler.Post(dto);
        if (status == 1)
        {
            return new StatusCodeResult(201);
        }
        else
        {
            return new StatusCodeResult(400);
        }
        
    }
}