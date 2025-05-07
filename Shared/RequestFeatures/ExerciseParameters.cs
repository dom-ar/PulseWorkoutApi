namespace Shared.RequestFeatures;

public class ExerciseParameters : RequestParameters
{
    public IEnumerable<int> BodyPartIds { get; set; } = new List<int>();
    public IEnumerable<int> CategoryIds { get; set; } = new List<int>();
}