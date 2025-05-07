namespace Entities.Exceptions;

public sealed class CollectionNotFoundException : NotFoundException
{
    public CollectionNotFoundException(string name, IEnumerable<int> ids) : base($"{name}(s) with id(s): {string.Join(", ", ids)} doesn't exist in the database.")
    { }
}