namespace Entities.Exceptions;

public sealed class ExerciseNotFoundForUserException : NotFoundException
{
    public ExerciseNotFoundForUserException(int id)
        : base($"The exercise with id: {id} doesn't exist in the database or doesn't belong to this user.")
    {
    }
}