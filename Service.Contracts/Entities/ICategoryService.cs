using Entities.Models;
using Shared.DataTransferObjects.Category;
using Shared.DataTransferObjects.Exercise;

namespace Service.Contracts.Entities;

public interface ICategoryService
{
    // GET
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges);
    Task<IEnumerable<CategoryDto>> GetCategoriesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    Task<CategoryDto> GetCategoryAsync(int id, bool trackChanges);
    // POST
    Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto category);
    // DELETE
    Task DeleteCategoryAsync(int id, bool trackChanges);
    // PUT
    Task UpdateCategoryAsync(int id, CategoryForUpdateDto categoryForUpdate, bool trackChanges);
}