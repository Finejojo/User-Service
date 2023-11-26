using Serilog;
using User_Service.Api.AppUser.Services.Interface;
using User_Service.Domain.AppUser.Models;
using UserMicroservices.Services.Interface;


namespace User_Service.Api;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
    private readonly IUserValidationService _userValidationService;
    private readonly IPasswordProvider _passwordProvider;

    public UserService(IUserRepository userRepository, IUserValidationService userValidationService, IPasswordProvider passwordProvider, IJwtTokenGeneratorService jwtTokenGeneratorService)
    {
        _userRepository = userRepository;
        _userValidationService = userValidationService;
        _passwordProvider = passwordProvider;
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
    }

    public async Task<string> CreateUser(CreateUserDto userDto)
    {
        try
        {
            var validationException = _userValidationService.ValidateCreateUser(userDto);
            if (validationException != null) throw validationException;

            var availableUser = await _userRepository.GetUserByUserName($"{userDto.UserName}");
            if (availableUser != null)
            {
                Log.Warning($"There is already a user found with the given username: {userDto.UserName}.");
                throw new ConflictException($"there is already a user found with the given username: {userDto.UserName}.");
            }

            var user = new User(userDto);
            var (passwordHash, passwordSalt) = _passwordProvider.CreatePasswordHash(userDto.Password);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return await _userRepository.CreateUser(user);
        }
        catch (BadRequestException e)
        {
            Log.Error($"Bad Request Error: {e.Message}");
            throw;
        }
        catch (ConflictException e)
        {
            Log.Error($"Conflict Error: {e.Message}");
            throw;
        }
        catch (AppException e)
        {
            Log.Error($"Database Error: {e.Message}");
            throw;
        }

        // General error (this should be last in the catch sequence)
        catch (Exception e)
        {
            Log.Error($"Error Creating User: {e.Message}");
            throw new InternalServerException(e.Message);
        }
    }

    public async Task<string> UpdateUser(string reference, UpdateUserDto userDto)
    {
        try
        {
            var validationException = _userValidationService.ValidateUpdateUser(userDto);
            if (validationException != null) throw validationException;

            await GetUserByReference(reference);

            var user = new User(userDto)
            {
                Reference = reference
            };

            var availableUser = await _userRepository.GetUserByUserName($"{userDto.UserName}");
            if (availableUser != null)
            {
                Log.Warning($"There is already a user found with the given username: {userDto.UserName}.");
                throw new ConflictException($"there is already a user found with the given username: {userDto.UserName}.");
            }


            var (passwordHash, passwordSalt) = _passwordProvider.CreatePasswordHash(userDto.Password);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


            return await _userRepository.UpdateUser(reference, user);
        }
        catch (BadRequestException e)
        {
            Log.Error($"Bad Request Error: {e.Message}");
            throw;
        }
        catch (AppException e)
        {
            Log.Error($"Database Error: {e.Message}");
            throw;
        }

        // General error (this should be last in the catch sequence)
        catch (Exception e)
        {
            Log.Error($"Error Updating User: {e.Message}");
            throw new InternalServerException(e.Message);
        }

    }

    public async Task<string> DeleteUser(string reference)
    {
        try
        {
            return await _userRepository.DeleteUser(reference);
        }
        catch (BadRequestException e)
        {
            Log.Error($"Bad Request Error: {e.Message}");
            throw;
        }
        catch (AppException e)
        {
            Log.Error($"Database Error: {e.Message}");
            throw;
        }

        // General error (this should be last in the catch sequence)
        catch (Exception e)
        {
            Log.Error($"Error Deleting User: {e.Message}");
            throw new InternalServerException(e.Message);
        }
    }

    public async Task<UserDto> GetUserByReference(string reference)
    {
        try
        {
            var user = await _userRepository.GetUserByReference(reference) ?? throw new NotFoundException("User not found by the given reference.");
            return new UserDto
            {
                Reference = user.Reference,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                UserName = user.UserName,

            };
        }
        catch (AppException)  // Catching known exceptions
        {
            throw;
        }
        catch (Exception e)  // Catching unexpected exceptions
        {
            throw new InternalServerException($"Error fetching user by reference: {e.Message}");
        }
    }
    public async Task<UserDto> GetUserByUserName(string username)
    {
        try
        {
            var user = await _userRepository.GetUserByUserName(username) ?? throw new NotFoundException("User not found by the given username.");
            return new UserDto
            {
                Reference = user.Reference,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                UserName = user.UserName,

            };
        }
        catch (AppException)  // Catching known exceptions
        {
            throw;
        }
        catch (Exception e)  // Catching unexpected exceptions
        {
            throw new InternalServerException($"Error fetching user by fullname: {e.Message}");
        }
    }

 


   
}

