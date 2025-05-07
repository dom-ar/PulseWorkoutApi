using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Exercise;

public record ExerciseForManipulationDto
{
    [Required(ErrorMessage = "Exercise Name is a required field")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Exercise name must be between 2 and 100 characters.")]
    public string? Name { get; init; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string? Description { get; init; }

    public List<string>? Instructions { get; init; }


    [Required(ErrorMessage = "BodyPartId is a required field.")]
    [Range(1, int.MaxValue, ErrorMessage = "BodyPartId must be greater than zero.")]
    public int? BodyPartId { get; init; }

    [Required(ErrorMessage = "CategoryId is a required field.")]
    [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be greater than zero.")]
    public int? CategoryId { get; init; }

    public string? IconUrl { get; init; }

    public List<string>? ImageUrls { get; init; }
}