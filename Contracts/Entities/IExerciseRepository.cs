using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts.Entities;

public interface IExerciseRepository
{
    // GET
    Task<PagedList<Exercise>> UserGetAllExercisesAsync(ExerciseParameters exerciseParameters, bool trackChanges, Guid userId);
    Task<PagedList<Exercise>> GetAllExercisesAsync(ExerciseParameters exerciseParameters, bool trackChanges);
    Task<Exercise?> UserGetExerciseAsync(int exerciseId, bool trackChanges, Guid userId);
    Task<Exercise?> GetExerciseAsync(int exerciseId, bool trackChanges);
    // POST
    void CreateExercise(Exercise exercise);
    // DELETE
    void DeleteExercise(Exercise exercise);
}