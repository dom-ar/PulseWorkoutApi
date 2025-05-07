using Entities.Models;

namespace Contracts.Entities;

public interface ICategoryRepository
{
    // GET
    Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
    Task<IEnumerable<Category>> GetCategoriesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    Task<Category?> GetCategoryAsync(int categoryId, bool trackChanges);
    // POST
    void CreateCategory(Category category);
    // DELETE
    void DeleteCategory(Category category);
}