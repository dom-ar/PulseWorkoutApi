namespace Entities.Exceptions;

public sealed class ExerciseNotFoundException : NotFoundException
{
    public ExerciseNotFoundException(int id)
        : base($"The exercise with id: {id} doesn't exist in the database")
    {
    }
}