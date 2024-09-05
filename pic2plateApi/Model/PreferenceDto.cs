namespace pic2plateApi.Model;

public record PreferenceDto
{
    public string PersonId { get; set; } = null!;
    
    public string Name { get; set; } = null!;
};