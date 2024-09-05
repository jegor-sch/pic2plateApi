namespace pic2plateApi.Model;

public record Preference
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public DateTime DeletionTime { get; init; }
};