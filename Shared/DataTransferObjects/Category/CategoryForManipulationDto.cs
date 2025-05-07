using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Category;

public record CategoryForManipulationDto
{
    [Required(ErrorMessage = "Category Name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Category Name is 60 characters")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Category must have one of the set ExerciseType: Weight, Distance, TimeOnly, RepsOnly")]
    [EnumDataType(typeof(ExerciseType), ErrorMessage = "Invalid ExerciseType value.")]
    public ExerciseType ExerciseType { get; init; }
}