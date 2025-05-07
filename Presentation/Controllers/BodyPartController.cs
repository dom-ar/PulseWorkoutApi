using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects.BodyPart;
using Shared.DataTransferObjects.Category;

namespace Presentation.Controllers;

[Route("api/bodyparts")]
[ApiController]
public class BodyPartController : ControllerBase
{
    private readonly IServiceManager _service;

    public BodyPartController(IServiceManager service) => _service = service;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllBodyParts()
    {
        var bodyParts = await _service.BodyPartService.GetAllBodyPartsAsync(trackChanges: false);
        return Ok(bodyParts);
    }

    [HttpGet("collection/({ids})", Name = "BodyPartCollection")]
    [Authorize]
    public async Task<IActionResult> GetBodyPartsByIds([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
    {
        var bodyParts = await _service.BodyPartService.GetBodyPartsByIdsAsync(ids, trackChanges: false);
        return Ok(bodyParts);
    }

    [HttpGet("{id:int}", Name = "BodyPartById")]
    [Authorize]
    public async Task<IActionResult> GetBodyPart(int id)
    {
        var bodyPart = await _service.BodyPartService.GetBodyPartAsync(id, trackChanges: false);
        return Ok(bodyPart);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateBodyPart([FromBody] BodyPartForCreationDto bodyPart)
    {
        var createdBodyPart = await _service.BodyPartService.CreateBodyPartAsync(bodyPart);

        return CreatedAtRoute("BodyPartById", new { id = createdBodyPart.Id }, createdBodyPart);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteBodyPart(int id)
    {
        await _service.BodyPartService.DeleteBodyPartAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateBodyPart(int id, [FromBody] BodyPartForUpdateDto bodyPart)
    {
        await _service.BodyPartService.UpdateBodyPartAsync(id, bodyPart, trackChanges: true);
        return NoContent();
    }
}