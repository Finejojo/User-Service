using Microsoft.AspNetCore.Mvc;
using UserMicroservices.Exceptions.Implimentation;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _userService;

    public AuthController(IAuthService userService)
    {
        _userService = userService;
    }

   
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto userDto)
    {
        try
        {
            var result = await _userService.Login(userDto);

            if (string.IsNullOrEmpty(result))
                throw new UnauthorizedAccessException("Invalid credentials."); //

            return Ok(result);
        }
        catch (AppException e)
        {
            return StatusCode(e.StatusCode, new AppExceptionResponse(e));
        }
    }

}


