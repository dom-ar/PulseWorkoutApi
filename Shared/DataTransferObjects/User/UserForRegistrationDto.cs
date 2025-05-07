using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.User;

public record UserForRegistrationDto
{
    [Required(ErrorMessage = "Username is required")]
    public string? UserName { get; init; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; init; }
    [Required(ErrorMessage = "Roles are required")]
    public ICollection<string>? Roles { get; init; }
}