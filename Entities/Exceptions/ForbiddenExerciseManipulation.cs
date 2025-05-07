namespace Entities.Exceptions;

public sealed class ForbiddenExerciseManipulation : ForbiddenException
{
    public ForbiddenExerciseManipulation(int id)
        : base($"The exercise with id: {id} is not created by this user and cannot be manipulated.")
    {
    }
}