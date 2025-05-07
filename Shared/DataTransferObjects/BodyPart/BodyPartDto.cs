namespace Shared.DataTransferObjects.BodyPart;
public record BodyPartDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
}