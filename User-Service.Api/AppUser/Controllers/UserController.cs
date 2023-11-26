using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMicroservices.Exceptions.Implimentation;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        try
        {
            var result = await _userService.CreateUser(createUserDto);
            return Ok(result);
        }
        catch (AppException e)
        {
            return StatusCode(e.StatusCode, new AppExceptionResponse(e));
        }
    }
    [Authorize]
    [HttpPut("{reference}")]
    public async Task<IActionResult> UpdateUser(string reference, [FromBody] UpdateUserDto updateUserDto)
    {
        try
        {
            var result = await _userService.UpdateUser(reference, updateUserDto);
            return Ok(result);
        }
        catch (AppException e)
        {
            return StatusCode(e.StatusCode, new AppExceptionResponse(e));
        }
    }
    [Authorize]
    [HttpDelete("{reference}")]
    public async Task<IActionResult> DeleteUser(string reference)
    {
        try
        {
            await GetUserByReference(reference);
            var result = await _userService.DeleteUser(reference);
            return Ok(result);
        }
        catch (AppException e)
        {
            return StatusCode(e.StatusCode, new AppExceptionResponse(e));
        }
    }
    [Authorize]
    [HttpGet("{reference}")]
    public async Task<IActionResult> GetUserByReference(string reference)
    {
        try
        {
            var result = await _userService.GetUserByReference(reference) ?? throw new NotFoundException($"No user found with reference: {reference}");
            return Ok(result);
        }
        catch (AppException e)
        {
            return StatusCode(e.StatusCode, new AppExceptionResponse(e));
        }
    }

    [HttpGet("username/{username}")]
    public async Task<IActionResult> GetUserByUserName(string username)
    {
        try
        {
            var result = await _userService.GetUserByUserName(username) ?? throw new NotFoundException($"No user found with username: {username}");
            return Ok(result);
        }
        catch (AppException e)
        {
            return StatusCode(e.StatusCode, new AppExceptionResponse(e));
        }
    }
   
   
   

}


