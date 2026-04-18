using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("exercises")]
public class ExerciseController : ControllerBase
{
    private readonly ExerciseService _exerciseService;

    public ExerciseController(ExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetExercises([FromQuery] string? search)
    {
        var exercise = await _exerciseService.GetExercises(search);

        return Ok(exercise);
    }


}