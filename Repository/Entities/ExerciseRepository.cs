using Contracts.Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository.Entities;

public class ExerciseRepository : RepositoryBase<Exercise>, IExerciseRepository
{
    public ExerciseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    // GET

    public async Task<PagedList<Exercise>> UserGetAllExercisesAsync(ExerciseParameters exerciseParameters,
        bool trackChanges, Guid userId)
    {
        var query = FindAll(trackChanges)
            .Include(e => e.BodyPart)
            .Include(e => e.Category)
            .Where(e => !e.IsUserCreated || e.CreatedByUserId == userId)
            .AsQueryable();

        if (exerciseParameters.CategoryIds.Any())
        {
            query = query.Where( e =>
                e.CategoryId > 0 && exerciseParameters.CategoryIds.Contains(e.CategoryId));
        }

        if (exerciseParameters.BodyPartIds.Any())
        {
            query = query.Where(e =>
                e.BodyPartId > 0 && exerciseParameters.BodyPartIds.Contains(e.BodyPartId));
        }

        var exercises = await query
            .OrderBy(e => e.Name)
            .ToListAsync();

        return PagedList<Exercise>.ToPagedList(exercises, exerciseParameters.PageNumber, exerciseParameters.PageSize);
    }

    public async Task<PagedList<Exercise>> GetAllExercisesAsync(ExerciseParameters exerciseParameters, bool trackChanges)
    {
        var query = FindAll(trackChanges)
            .Include(e => e.BodyPart)
            .Include(e => e.Category)
            .AsQueryable();

        if (exerciseParameters.CategoryIds.Any())
        {
            query = query.Where(e =>
                e.CategoryId > 0 && exerciseParameters.CategoryIds.Contains(e.CategoryId));
        }

        if (exerciseParameters.BodyPartIds.Any())
        {
            query = query.Where(e =>
                e.BodyPartId > 0 && exerciseParameters.BodyPartIds.Contains(e.BodyPartId));
        }

        var exercises = await query
            .OrderBy(e => e.Name)
            .ToListAsync();

        return PagedList<Exercise>.ToPagedList(exercises, exerciseParameters.PageNumber, exerciseParameters.PageSize);
    }

    public async Task<Exercise?> UserGetExerciseAsync(int exerciseId, bool trackChanges, Guid userId) =>
        await FindByCondition(e => e.Id == exerciseId && (!e.IsUserCreated || e.CreatedByUserId == userId), trackChanges)
            .Include(e => e.BodyPart)
            .Include(e => e.Category)
            .SingleOrDefaultAsync();

    public async Task<Exercise?> GetExerciseAsync(int exerciseId, bool trackChanges) =>
        await FindByCondition(e => e.Id.Equals(exerciseId), trackChanges)
            .Include(e => e.BodyPart)
            .Include(e => e.Category)
            .SingleOrDefaultAsync();

    // POST
    public void CreateExercise(Exercise exercise)
    {
        Create(exercise);
    }

    // DELETE
    public void DeleteExercise(Exercise exercise) => Delete(exercise);
}

