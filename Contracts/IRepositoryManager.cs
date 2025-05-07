using Contracts.Entities;

namespace Contracts;

public interface IRepositoryManager
{
    IExerciseRepository Exercise { get; }
    ICategoryRepository Category { get; }
    IBodyPartRepository BodyPart { get; }
    IAvatarRepository Avatar { get; }

    Task SaveAsync();
}