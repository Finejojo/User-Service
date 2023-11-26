using User_Service.Api.AppUser.Services.Interface;
using UserMicroservices.Services.Interface;

namespace User_Service.Api.AppUser.Services.Implimentation;

public class UserValidationService : IUserValidationService
{

    public AppException ValidateCreateUser(CreateUserDto createUserDto)
    {
        return new ErrorService().GetValidationExceptionResult(new CreateUserValidator().Validate(createUserDto));
    }
    public AppException ValidateUpdateUser(UpdateUserDto updateUserDto)
    {
        return new ErrorService().GetValidationExceptionResult(new UpdateUserValidator().Validate(updateUserDto));
    }
}