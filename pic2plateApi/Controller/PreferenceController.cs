using Microsoft.AspNetCore.Mvc;
using pic2plateApi.Handler;

namespace pic2plateApi.Controller;

public class PreferenceController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly PreferenceHandler _preferenceHandler;

    public PreferenceController(PreferenceHandler preferenceHandler)
    {
        _preferenceHandler = preferenceHandler;
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        return await _preferenceHandler.Get();
    }
}