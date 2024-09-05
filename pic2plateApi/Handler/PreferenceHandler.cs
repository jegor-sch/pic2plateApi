using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        try
        {
            if (dto.Names.Count == 0)
            {
                await _preferenceRepository.Delete(dto.PersonId);
                return 1;
            }

            foreach (var name in dto.Names)
            {
                await _preferenceRepository.Post(dto.PersonId, name);
            }

            return 1;
        }
        catch (Exception e)
        {
            return 0;
        }
    }
}