


    public interface IUserService
    {

        Task<string> CreateUser(CreateUserDto user);
        Task<string> UpdateUser(string reference, UpdateUserDto user);
        Task<string> DeleteUser(string reference);
        Task<UserDto> GetUserByReference(string reference);
    
        Task<UserDto> GetUserByUserName(string username);
    }

