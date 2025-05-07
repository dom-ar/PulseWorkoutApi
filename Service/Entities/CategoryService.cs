using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts.Entities;
using Shared.DataTransferObjects.Category;
using Shared.DataTransferObjects.Exercise;

namespace Service.Entities;

internal sealed class CategoryService : ICategoryService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public CategoryService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    // GET

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges)
    {
            var categories = await _repository.Category.GetAllCategoriesAsync(trackChanges);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesByIdsAsync(IEnumerable<int> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var categoryEntities = await _repository.Category.GetCategoriesByIdsAsync(ids, trackChanges);
        if (ids.Count() != categoryEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var categoriesToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);
        return categoriesToReturn;
    }

    public async Task<CategoryDto> GetCategoryAsync(int id, bool trackChanges)
    {
        var category = await _repository.Category.GetCategoryAsync(id, trackChanges);
        if (category is null)
        {
            throw new UniversalNotFoundException("category", id);
        }

        var categoryDto = _mapper.Map<CategoryDto>(category);
        return categoryDto;
    }

    // POST

    public async Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto category)
    {
        var categoryEntity = _mapper.Map<Category>(category);

        _repository.Category.CreateCategory(categoryEntity);
        await _repository.SaveAsync();

        var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);
        return categoryToReturn;
    }

    // DELETE

    public async Task DeleteCategoryAsync(int id, bool trackChanges)
    {
        var category = await _repository.Category.GetCategoryAsync(id, trackChanges);
        if (category is null)
            throw new UniversalNotFoundException("category", id);

        _repository.Category.DeleteCategory(category);
        await _repository.SaveAsync();
    }

    // PUT
    public async Task UpdateCategoryAsync(int id, CategoryForUpdateDto categoryForUpdate, bool trackChanges)
    {
        var categoryEntity = await _repository.Category.GetCategoryAsync(id, trackChanges);
        if (categoryEntity is null)
            throw new UniversalNotFoundException("category", id);

        _mapper.Map(categoryForUpdate, categoryEntity);
        await _repository.SaveAsync();
    }
}