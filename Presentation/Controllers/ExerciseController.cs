using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.Exercise;
using Shared.RequestFeatures;

namespace Presentation.Controllers;

[Route("api/exercises")]
[ApiController]
public class ExerciseController : ControllerBase
{
    private readonly IServiceManager _service;

    public ExerciseController(IServiceManager service) => _service = service;

    // GET FOR USER
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> UserGetAllExercises([FromQuery] ExerciseParameters exerciseParameters)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("User ID Claim is missing");
        }
        var userId = Guid.Parse(userIdClaim);

        var pagedResult = await _service.ExerciseService.UserGetAllExercisesAsync(exerciseParameters, trackChanges: false, userId);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.exercises);
    }

    [HttpGet("{id:int}", Name = "ExerciseById")]
    [Authorize]
    public async Task<IActionResult> UserGetExercise(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("User ID Claim is missing");
        }
        var userId = Guid.Parse(userIdClaim);

        var exercise = await _service.ExerciseService.UserGetExerciseAsync(id, trackChanges: false, userId);
        return Ok(exercise);
    }

    // GET FOR ADMIN
    [HttpGet("admin")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetAllExercises([FromQuery] ExerciseParameters exerciseParameters)
    {
        var pagedResult = await _service.ExerciseService.GetAllExercisesAsync(exerciseParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.exercises);
    }

    [HttpGet("admin/{id:int}", Name = "AdminExerciseById")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetExercise(int id)
    {
        var exercise = await _service.ExerciseService.GetExerciseAsync(id, trackChanges: false);
        return Ok(exercise);
    }

    // POST FOR USER
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize]
    public async Task<IActionResult> UserCreateExercise([FromBody] UserExerciseForCreationDto exercise)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("User ID Claim is missing");
        }
        var userId = Guid.Parse(userIdClaim);

        var createdExercise = await _service.ExerciseService.UserCreateExerciseAsync(exercise, userId);

        return CreatedAtRoute("ExerciseById", new { id = createdExercise.Id }, createdExercise);
    }

    // POST FOR ADMIN
    [HttpPost("admin")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateExercise([FromBody] ExerciseForCreationDto exercise)
    {
        var createdExercise = await _service.ExerciseService.CreateExerciseAsync(exercise);

        return CreatedAtRoute("ExerciseById", new { id = createdExercise.Id }, createdExercise);
    }

    // DELETE FOR USER
    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> UserDeleteExercise(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("User ID Claim is missing");
        }
        var userId = Guid.Parse(userIdClaim);

        await _service.ExerciseService.UserDeleteExerciseAsync(id, trackChanges: false, userId);
        return NoContent();
    }

    // DELETE FOR ADMIN
    [HttpDelete("admin/{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteExercise(int id)
    {
        await _service.ExerciseService.DeleteExerciseAsync(id, trackChanges: false);
        return NoContent();
    }

    // PUT FOR USER
    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize]
    public async Task<IActionResult> UserUpdateExercise(int id, [FromBody] UserExerciseForUpdateDto exercise)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("User ID Claim is missing");
        }
        var userId = Guid.Parse(userIdClaim);

        await _service.ExerciseService.UserUpdateExerciseAsync(id, exercise, trackChanges: true, userId);
        return NoContent();
    }

    // PUT FOR ADMIN

    [HttpPut("admin/{id:int}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateExercise(int id, [FromBody] ExerciseForUpdateDto exercise)
    {
        await _service.ExerciseService.UpdateExerciseAsync(id, exercise, trackChanges: true);
        return NoContent();
    }
}