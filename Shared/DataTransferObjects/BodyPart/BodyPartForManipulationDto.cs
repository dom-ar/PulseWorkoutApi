using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.BodyPart;

public record BodyPartForManipulationDto
{
    [Required(ErrorMessage = "BodyPart Name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the BodyPart Name is 60 characters")]
    public string? Name { get; init; }
}