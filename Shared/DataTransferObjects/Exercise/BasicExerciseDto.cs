using Shared.DataTransferObjects.BodyPart;
using Shared.DataTransferObjects.Category;

namespace Shared.DataTransferObjects.Exercise;

/// <summary>
/// Represents a basic exercise information with BodyPart and Category objects as well. Used when displaying all exercises in the exercise list 
/// </summary>
public record BasicExerciseDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public BodyPartDto? BodyPart { get; init; }
    public CategoryDto? Category { get; init; }
    public string? IconUrl { get; init; }
    public bool? IsUserCreated { get; init; }
}
