namespace pic2plateApi.Model;

public record PreferenceDto
{
    public string PersonId { get; set; } = null!;
    
    public List<string> Names { get; set; } = null!;
};