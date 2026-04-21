using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> PostUserRegister(UserRegisterDTO registerDTO)
    {
        var user = await _userService.UserRegister(registerDTO);

        if (user == null)
            return BadRequest("Email already exists.");

        return Created($"/{user.Id}", user);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> PostUserLogin(UserLoginDTO loginDTO)
    {
        var user = await _userService.UserLogin(loginDTO);

        if (user == null)
            return Unauthorized("Invalid email or password.");

        return Ok(user);
    }

    [HttpPut("me")]
    public async Task<IActionResult?> PutUser(UserResponseDTO dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var updatedUser = await _userService.UpdateUser(userId, dto.Name);

        if (updatedUser == null)
            return NotFound();

        return Ok(updatedUser);
    }

    [HttpDelete("me")]
    public async Task<IActionResult?> DeleteUser()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var deletedUser = await _userService.DeleteUser(userId);

        if (deletedUser == null)
            return NotFound();

        return NoContent();
    }
}