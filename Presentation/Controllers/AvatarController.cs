using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.Avatar;
using Shared.DataTransferObjects.Category;

namespace Presentation.Controllers;

[Route("api/avatars")]
[ApiController]
public class AvatarController : ControllerBase
{
    private readonly IServiceManager _service;

    public AvatarController(IServiceManager service) => _service = service;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAvatars()
    {
        var avatars = await _service.AvatarService.GetAllAvatarsAsync(trackChanges: false);
        return Ok(avatars);
    }

    [HttpGet("{id:int}", Name = "AvatarById")]
    [Authorize]
    public async Task<IActionResult> GetAvatar(int id)
    {
        var avatar = await _service.AvatarService.GetAvatarAsync(id, trackChanges: false);
        return Ok(avatar);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateAvatar([FromBody] AvatarForCreationDto avatar)
    {
        var createdAvatar = await _service.AvatarService.CreateAvatarAsync(avatar);

        return CreatedAtRoute("AvatarById", new { id = createdAvatar.Id }, createdAvatar);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteAvatar(int id)
    {
        await _service.AvatarService.DeleteAvatarAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateAvatar(int id, [FromBody] AvatarForUpdateDto avatar)
    {
        await _service.AvatarService.UpdateAvatarAsync(id, avatar, trackChanges: true);
        return NoContent();
    }
}