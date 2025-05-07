namespace Shared.DataTransferObjects.Avatar;

public record AvatarDto
{
    public int Id { get; init; }
    public string? ImageUrl { get; init; }
}