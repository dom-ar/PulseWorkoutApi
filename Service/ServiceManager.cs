using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Service.Contracts.Entities;
using Service.Entities;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IExerciseService> _exerciseService;
    private readonly Lazy<ICategoryService> _categoryService;
    private readonly Lazy<IBodyPartService> _bodyPartService;
    private readonly Lazy<IAvatarService> _avatarService;
    private readonly Lazy<IAuthenticationService> _authenticationService;

    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _exerciseService = new Lazy<IExerciseService>(() =>
            new ExerciseService(repositoryManager, logger, mapper));
        _categoryService = new Lazy<ICategoryService>(() =>
            new CategoryService(repositoryManager, logger, mapper));
        _bodyPartService = new Lazy<IBodyPartService>(() =>
            new BodyPartService(repositoryManager, logger, mapper));
        _avatarService = new Lazy<IAvatarService>(() =>
            new AvatarService(repositoryManager, logger, mapper));
        _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationService(logger, mapper, userManager, configuration));
    }

    public IExerciseService ExerciseService => _exerciseService.Value;
    public ICategoryService CategoryService => _categoryService.Value;
    public IBodyPartService BodyPartService => _bodyPartService.Value;
    public IAvatarService AvatarService => _avatarService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}