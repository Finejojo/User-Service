using User_Service.Api.AppUser.Services.Interface;
using UserMicroservices.Services.Interface;
using User_Service.Domain.AppUser.Models;

namespace User_Service.Api;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
    private readonly IUserValidationService _userValidationService;
    private readonly IPasswordProvider _passwordProvider;

    public AuthService(IUserRepository userRepository, IUserValidationService userValidationService, IPasswordProvider passwordProvider, IJwtTokenGeneratorService jwtTokenGeneratorService)
    {
        _userRepository = userRepository;
        _userValidationService = userValidationService;
        _passwordProvider = passwordProvider;
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
    }


   
    public async Task<string?> Login(LoginUserDto userDto)
    {
        var user = await _userRepository.GetUserByUserName(userDto.UserName);
        if (user == null || !_passwordProvider.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
            return null;

        return _jwtTokenGeneratorService.GenerateToken(user);
    }

  
}

