using System;

namespace pic2plateApi.Model;

public record RecipeDto
{
    public string PersonId { get; set; } = null!;
    
    public string Title { get; set; } = null!;

    public string Recipe { get; set; } = null!;
}