using Service.Contracts.Entities;

namespace Service.Contracts;

public interface IServiceManager
{
    IExerciseService ExerciseService { get; }
    ICategoryService CategoryService { get; }
    IBodyPartService BodyPartService { get; }
    IAvatarService AvatarService { get; }
    IAuthenticationService AuthenticationService { get; }
}