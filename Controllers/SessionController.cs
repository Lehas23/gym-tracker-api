using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("sessions")]
public class SessionController : ControllerBase
{
    private readonly SessionService _sessionService;

    public SessionController(SessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpGet("{sessionId}")]
    public async Task<IActionResult> GetSessionById(int sessionId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var session = await _sessionService.GetSessionById(userId, sessionId);

        if (session == null)
            return NotFound();

        return Ok(session);
    }

    [HttpGet]
    public async Task<IActionResult> GetLast10Sessions()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var sessions = await _sessionService.GetLast10Sessions(userId);

        return Ok(sessions);
    }

    [HttpPost("{templateId}")]
    public async Task<IActionResult> PostSession(int templateId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var session = await _sessionService.CreateSession(userId, templateId);

        if (session == null)
            return NotFound();

        return Ok(session);
    }

    [HttpPut("{sessionId}/sets/{setId}")]
    public async Task<IActionResult> PutSession(int sessionId, int setId, UpdateSetDTO dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var session = await _sessionService.UpdateSetDuringSession(userId, sessionId, setId, dto);

        if (session == null)
            return NotFound();

        return Ok(session);
    }

    [HttpDelete("{sessionId}/sets/{setId}")]
    public async Task<IActionResult> DeleteSetDuringSession(int sessionId, int setId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var session = await _sessionService.DeleteSetDuringSession(userId, sessionId, setId);

        if (session == null)
            return NotFound();

        return NoContent();
    }
}