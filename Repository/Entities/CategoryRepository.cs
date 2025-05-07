using Contracts.Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Entities;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    // GET
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

    public async Task<IEnumerable<Category>> GetCategoriesByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
        await FindByCondition(c => ids.Contains(c.Id), trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

    public async Task<Category?> GetCategoryAsync(int categoryId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(categoryId), trackChanges)
            .SingleOrDefaultAsync();

    // POST

    public void CreateCategory(Category category)
    {
        Create(category);
    }

    // DELETE

    public void DeleteCategory(Category category) => Delete(category);
}