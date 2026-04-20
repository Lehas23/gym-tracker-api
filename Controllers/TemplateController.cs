using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("templates")]
public class TemplateController : ControllerBase
{
    private readonly TemplateService _templateService;

    public TemplateController(TemplateService templateService)
    {
        _templateService = templateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTemplates()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var templates = await _templateService.GetTemplates(userId);

        return Ok(templates);
    }

    [HttpPost]
    public async Task<IActionResult> PostTemplate(CreateTemplateDTO dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var addTemplate = await _templateService.AddTemplate(userId, dto);
        return Created($"/templates/{addTemplate.Id}", addTemplate);
    }

    [HttpPost("{id}/exercises")]
    public async Task<IActionResult> PostExerciseToTemplate(int templateId, CreateTemplateExerciseDTO dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var exercise = await _templateService.AddExerciseToTemplate(userId, templateId, dto);

        if (exercise == null)
            return NotFound();

        return Ok(exercise);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTemplate(int id, [FromBody] string name)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var updatedTemplate = await _templateService.UpdateTemplate(userId, id, name);

        if (updatedTemplate == null)
            return NotFound();

        return Ok(updatedTemplate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTemplate(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var deletedTemplate = await _templateService.DeleteTemplate(userId, id);

        if (deletedTemplate == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}/exercises/{exerciseId}")]
    public async Task<IActionResult> DeleteExerciseFromTemplate(int id, int exerciseId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var exercise = await _templateService.DeleteExerciseFromTemplate(userId, id, exerciseId);

        if (exercise == null)
            return NotFound();

        return NoContent();
    }
}