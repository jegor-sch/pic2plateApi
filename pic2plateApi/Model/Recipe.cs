namespace pic2plateApi.Model;

public record Recipe
{
    public int Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Text { get; set; }
    
    public DateTime DeletionTime { get; set; }
};