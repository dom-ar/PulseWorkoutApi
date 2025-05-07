namespace Shared.DataTransferObjects.Category;
public record CategoryDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public ExerciseType ExerciseType { get; init; }
}

public enum ExerciseType
{
    Weight,
    Distance,
    TimeOnly,
    RepsOnly
}