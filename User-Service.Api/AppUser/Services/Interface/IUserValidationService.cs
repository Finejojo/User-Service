namespace User_Service.Api.AppUser.Services.Interface;

public interface IUserValidationService
{

    AppException ValidateCreateUser(CreateUserDto createUserDto);
    AppException ValidateUpdateUser(UpdateUserDto updateUserDto);

}

