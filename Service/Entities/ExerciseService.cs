using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts.Entities;
using Shared.DataTransferObjects.Exercise;
using Shared.RequestFeatures;

namespace Service.Entities;

internal sealed class ExerciseService : IExerciseService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public ExerciseService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    // GET

    public async Task<(IEnumerable<BasicExerciseDto> exercises, MetaData metaData)> UserGetAllExercisesAsync(ExerciseParameters exerciseParameters, bool trackChanges, Guid userId)
    {
        if (exerciseParameters.CategoryIds.Any())
        {
            var categories = await _repository.Category.GetCategoriesByIdsAsync(exerciseParameters.CategoryIds, false);
            var invalidCategories = exerciseParameters.CategoryIds.Except(categories.Select(c => c.Id));

            var invalidCategoriesList = invalidCategories.ToList();
            if (invalidCategoriesList.Any())
                throw new CollectionNotFoundException("category", invalidCategoriesList);
        }

        if (exerciseParameters.BodyPartIds.Any())
        {
            var bodyParts = await _repository.BodyPart.GetBodyPartsByIdsAsync(exerciseParameters.BodyPartIds, false);
            var invalidBodyParts = exerciseParameters.BodyPartIds.Except(bodyParts.Select(b => b.Id));

            var invalidBodyPartsList = invalidBodyParts.ToList();
            if (invalidBodyPartsList.Any())
                throw new CollectionNotFoundException("bodyPart", invalidBodyPartsList);
        }

        var exercisesWithMetaData = await _repository.Exercise.UserGetAllExercisesAsync(exerciseParameters, trackChanges, userId);
        var exercisesDto = _mapper.Map<IEnumerable<BasicExerciseDto>>(exercisesWithMetaData);
        return (exercises: exercisesDto, metaData: exercisesWithMetaData.MetaData);
    }

    public async Task<(IEnumerable<BasicExerciseDto> exercises, MetaData metaData)> GetAllExercisesAsync(ExerciseParameters exerciseParameters, bool trackChanges)
    {
        if (exerciseParameters.CategoryIds.Any())
        {
            var categories = await _repository.Category.GetCategoriesByIdsAsync(exerciseParameters.CategoryIds, false);
            var invalidCategories = exerciseParameters.CategoryIds.Except(categories.Select(c => c.Id));

            var invalidCategoriesList = invalidCategories.ToList();
            if (invalidCategoriesList.Any())
                throw new CollectionNotFoundException("category", invalidCategoriesList);
        }

        if (exerciseParameters.BodyPartIds.Any())
        {
            var bodyParts = await _repository.BodyPart.GetBodyPartsByIdsAsync(exerciseParameters.BodyPartIds, false);
            var invalidBodyParts = exerciseParameters.BodyPartIds.Except(bodyParts.Select(b => b.Id));

            var invalidBodyPartsList = invalidBodyParts.ToList();
            if (invalidBodyPartsList.Any())
                throw new CollectionNotFoundException("bodyPart", invalidBodyPartsList);
        }

        var exercisesWithMetaData = await _repository.Exercise.GetAllExercisesAsync(exerciseParameters, trackChanges);
        var exercisesDto = _mapper.Map<IEnumerable<BasicExerciseDto>>(exercisesWithMetaData);
        return (exercises: exercisesDto, metaData: exercisesWithMetaData.MetaData);
    }

    public async Task<FullExerciseDto> UserGetExerciseAsync(int id, bool trackChanges, Guid userId)
    {
        var exercise = await _repository.Exercise.UserGetExerciseAsync(id, trackChanges, userId);
        if (exercise is null)
        {
            throw new ExerciseNotFoundForUserException(id);
        }

        var exerciseDto = _mapper.Map<FullExerciseDto>(exercise);
        return exerciseDto;
    }

    public async Task<FullExerciseDto> GetExerciseAsync(int id, bool trackChanges)
    {
        var exercise = await _repository.Exercise.GetExerciseAsync(id, trackChanges);
        if (exercise is null)
        {
            throw new ExerciseNotFoundException(id);
        }

        var exerciseDto = _mapper.Map<FullExerciseDto>(exercise);
        return exerciseDto;
    }

    // POST

    public async Task<FullExerciseDto> UserCreateExerciseAsync(UserExerciseForCreationDto exercise, Guid userId)
    {
        var exerciseEntity = _mapper.Map<Exercise>(exercise);

        exerciseEntity.CategoryId = exercise.CategoryId ?? 1;
        exerciseEntity.BodyPartId = exercise.BodyPartId ?? 1;
        exerciseEntity.IsUserCreated = true;
        exerciseEntity.CreatedByUserId = userId;

        var categoryEntity = await _repository.Category.GetCategoryAsync(exerciseEntity.CategoryId, false);
        if (categoryEntity is null)
            throw new UniversalNotFoundException("category", exerciseEntity.CategoryId);

        var bodyPartEntity = await _repository.BodyPart.GetBodyPartAsync(exerciseEntity.BodyPartId, false);
        if (bodyPartEntity is null)
            throw new UniversalNotFoundException("bodyPart", exerciseEntity.BodyPartId);

        _repository.Exercise.CreateExercise(exerciseEntity);
        await _repository.SaveAsync();

        var exerciseToReturn = _mapper.Map<FullExerciseDto>(exerciseEntity);

        return exerciseToReturn;
    }

    public async Task<FullExerciseDto> CreateExerciseAsync(ExerciseForCreationDto exercise)
    {
        var exerciseEntity = _mapper.Map<Exercise>(exercise);

        exerciseEntity.CategoryId = exercise.CategoryId ?? 1;
        exerciseEntity.BodyPartId = exercise.BodyPartId ?? 1;
        exerciseEntity.IsUserCreated = false;
        exerciseEntity.CreatedByUserId = null;

        var categoryEntity = await _repository.Category.GetCategoryAsync(exerciseEntity.CategoryId, false);
        if (categoryEntity is null)
            throw new UniversalNotFoundException("category", exerciseEntity.CategoryId);

        var bodyPartEntity = await _repository.BodyPart.GetBodyPartAsync(exerciseEntity.BodyPartId, false);
        if (bodyPartEntity is null)
            throw new UniversalNotFoundException("bodyPart", exerciseEntity.BodyPartId);

        _repository.Exercise.CreateExercise(exerciseEntity);
        await _repository.SaveAsync();

        var exerciseToReturn = _mapper.Map<FullExerciseDto>(exerciseEntity);

        return exerciseToReturn;
    }

    // DELETE

    public async Task UserDeleteExerciseAsync(int id, bool trackChanges, Guid userId)
    {
        var exercise = await _repository.Exercise.UserGetExerciseAsync(id, trackChanges, userId);
        if (exercise is null)
            throw new ExerciseNotFoundForUserException(id);

        if (!exercise.IsUserCreated)
            throw new ForbiddenExerciseManipulation(id);

        _repository.Exercise.DeleteExercise(exercise);
        await _repository.SaveAsync();
    }

    public async Task DeleteExerciseAsync(int id, bool trackChanges)
    {
        var exercise = await _repository.Exercise.GetExerciseAsync(id, trackChanges);
        if (exercise is null)
            throw new UniversalNotFoundException("exercise", id);

        _repository.Exercise.DeleteExercise(exercise);
        await _repository.SaveAsync();
    }

    // PUT
    public async Task UserUpdateExerciseAsync(int id, UserExerciseForUpdateDto exerciseForUpdate, bool trackChanges, Guid userId)
    {
        var exerciseEntity = await _repository.Exercise.UserGetExerciseAsync(id, trackChanges, userId);
        if (exerciseEntity is null)
            throw new ExerciseNotFoundForUserException(id);

        if (exerciseEntity.IsUserCreated is false)
            throw new ForbiddenExerciseManipulation(id);

        if (exerciseForUpdate.BodyPartId.HasValue)
        {
            var bodyPart = await _repository.BodyPart.GetBodyPartAsync(exerciseForUpdate.BodyPartId.Value, trackChanges: false);
            if (bodyPart is null)
                throw new UniversalNotFoundException("bodyPart", id);
        }

        if (exerciseForUpdate.CategoryId.HasValue)
        {
            var category = await _repository.Category.GetCategoryAsync(exerciseForUpdate.CategoryId.Value, trackChanges: false);
            if (category is null)
                throw new UniversalNotFoundException("category", id);
        }

        _mapper.Map(exerciseForUpdate, exerciseEntity);
        await _repository.SaveAsync();
    }


    public async Task UpdateExerciseAsync(int id, ExerciseForUpdateDto exerciseForUpdate, bool trackChanges)
    {
        var exerciseEntity = await _repository.Exercise.GetExerciseAsync(id, trackChanges);
        if (exerciseEntity is null)
            throw new ExerciseNotFoundException(id);

        if (exerciseForUpdate.BodyPartId.HasValue)
        {
            var bodyPart = await _repository.BodyPart.GetBodyPartAsync(exerciseForUpdate.BodyPartId.Value, trackChanges: false);
            if (bodyPart is null)
                throw new UniversalNotFoundException("bodyPart", id);
        }

        if (exerciseForUpdate.CategoryId.HasValue)
        {
            var category = await _repository.Category.GetCategoryAsync(exerciseForUpdate.CategoryId.Value, trackChanges: false);
            if (category is null)
                throw new UniversalNotFoundException("category", id);
        }

        _mapper.Map(exerciseForUpdate, exerciseEntity);
        await _repository.SaveAsync();
    }
}