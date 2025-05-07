using Contracts;
using Contracts.Entities;
using Repository.Entities;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IExerciseRepository> _exerciseRepository;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<IBodyPartRepository> _bodyPartRepository;
    private readonly Lazy<IAvatarRepository> _avatarRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _exerciseRepository = new Lazy<IExerciseRepository>(() =>
            new ExerciseRepository(repositoryContext));
        _categoryRepository = new Lazy<ICategoryRepository>(() =>
            new CategoryRepository(repositoryContext));
        _bodyPartRepository = new Lazy<IBodyPartRepository>(() =>
            new BodyPartRepository(repositoryContext));
        _avatarRepository = new Lazy<IAvatarRepository>(() =>
            new AvatarRepository(repositoryContext));
    }

    public IExerciseRepository Exercise => _exerciseRepository.Value;
    public ICategoryRepository Category => _categoryRepository.Value;
    public IBodyPartRepository BodyPart => _bodyPartRepository.Value;
    public IAvatarRepository Avatar => _avatarRepository.Value;

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}