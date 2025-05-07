using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects.Category;
using Shared.DataTransferObjects.Exercise;

namespace Presentation.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IServiceManager _service;

    public CategoryController(IServiceManager service) => _service = service;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _service.CategoryService.GetAllCategoriesAsync(trackChanges: false);
        return Ok(categories);
    }

    [HttpGet("collection/({ids})", Name = "CategoryCollection")]
    [Authorize]
    public async Task<IActionResult> GetCategoriesByIds([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
    {
        var categories = await _service.CategoryService.GetCategoriesByIdsAsync(ids, trackChanges: false);
        return Ok(categories);
    }

    [HttpGet("{id:int}", Name = "CategoryById")]
    [Authorize]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await _service.CategoryService.GetCategoryAsync(id, trackChanges: false);
        return Ok(category);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationDto category)
    {
        var createdCategory = await _service.CategoryService.CreateCategoryAsync(category);

        return CreatedAtRoute("CategoryById", new { id = createdCategory.Id }, createdCategory);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _service.CategoryService.DeleteCategoryAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryForUpdateDto category)
    {
        await _service.CategoryService.UpdateCategoryAsync(id, category, trackChanges: true);
        return NoContent();
    }
}