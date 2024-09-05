using pic2plateApi.Model;
using pic2plateApi.Repository;

namespace pic2plateApi.Handler;

public class PreferenceHandler
{
    
    public readonly PreferenceRepository _preferenceRepository;

    public PreferenceHandler(PreferenceRepository preferenceRepository)
    {
        _preferenceRepository = preferenceRepository;
    }

    public async Task<List<Preference>> Get(string personId)
    {
        return await _preferenceRepository.Get(personId);
    }

    public async Task<int> Post(PreferenceDto dto)
    {
        return await _preferenceRepository.Post(dto.PersonId, dto.Name);
    }
}