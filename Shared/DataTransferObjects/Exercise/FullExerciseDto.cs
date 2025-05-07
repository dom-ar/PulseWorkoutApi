using Shared.DataTransferObjects.BodyPart;
using Shared.DataTransferObjects.Category;

namespace Shared.DataTransferObjects.Exercise;

/// <summary>
/// Represents full exercise information with BodyPart and Category objects. Used when viewing a single exercise.
/// </summary>
public record FullExerciseDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public List<string>? Instructions { get; init; }
    public BodyPartDto? BodyPart { get; init; }
    public CategoryDto? Category { get; init; }
    public string? IconUrl { get; init; }
    public List<string>? ImageUrls { get; init; }
    public Guid? CreatedByUserId { get; init; }
    public bool? IsUserCreated { get; init; }

}