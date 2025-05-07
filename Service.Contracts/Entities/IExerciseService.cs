using Entities.Models;
using Shared.DataTransferObjects.Exercise;
using Shared.RequestFeatures;

namespace Service.Contracts.Entities;

public interface IExerciseService
{
    // GET
    Task<(IEnumerable<BasicExerciseDto> exercises, MetaData metaData)> UserGetAllExercisesAsync(ExerciseParameters exerciseParameters, bool trackChanges, Guid userId);
    Task<(IEnumerable<BasicExerciseDto> exercises, MetaData metaData)> GetAllExercisesAsync(ExerciseParameters exerciseParameters, bool trackChanges);
    Task<FullExerciseDto> UserGetExerciseAsync(int id, bool trackChanges, Guid userId);
    Task<FullExerciseDto> GetExerciseAsync(int id, bool trackChanges);
    // POST
    Task<FullExerciseDto> CreateExerciseAsync(ExerciseForCreationDto exercise);
    Task<FullExerciseDto> UserCreateExerciseAsync(UserExerciseForCreationDto exercise, Guid userId);
    // DELETE
    Task DeleteExerciseAsync(int id, bool trackChanges);
    Task UserDeleteExerciseAsync(int id, bool trackChanges, Guid userId);
    // PUT
    Task UpdateExerciseAsync(int id, ExerciseForUpdateDto exerciseForUpdate, bool trackChanges);
    Task UserUpdateExerciseAsync(int id, UserExerciseForUpdateDto exerciseForUpdate, bool trackChanges, Guid userId);
}