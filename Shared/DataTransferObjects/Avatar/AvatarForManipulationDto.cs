using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Avatar;

public record AvatarForManipulationDto
{
    [Required(ErrorMessage = "Image URL is a required field.")]
    [Url(ErrorMessage = "Invalid URL format.")]
    public string? ImageUrl { get; init; }
}