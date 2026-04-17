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

    [HttpGet]
    public async Task<IActionResult> GetLast10Sessions()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var sessions = await _sessionService.GetSessions(userId);

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
}