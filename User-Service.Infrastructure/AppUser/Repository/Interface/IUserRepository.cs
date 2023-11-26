using User_Service.Domain.AppUser.Models;

public interface IUserRepository
{
    Task<string> CreateUser(User user);
    Task<string> UpdateUser(string reference, User user);
    Task<string> DeleteUser(string reference);
    Task<User> GetUserByReference(string reference);
    Task<User> GetUserByUserName(string userName);

}