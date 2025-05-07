namespace Entities.Exceptions;

public sealed class UniversalNotFoundException : NotFoundException
{
    public UniversalNotFoundException(string name, int? id)
        : base($"The {name} with id: {id} doesn't exist in the database")
    {
    }
}