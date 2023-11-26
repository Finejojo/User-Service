using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace User_Service.Domain.AppUser.Models;
public class User
{
    [BsonId]
    public string Reference { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; private set; }
    public string UserName { get; set; }
    public string? PasswordHash { get; set; } 
    public byte[]? PasswordSalt { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public User(CreateUserDto createUserDto)
    {
        Reference = Guid.NewGuid().ToString();
        FirstName = createUserDto.FirstName;
        LastName = createUserDto.LastName;
        FullName = $"{LastName}, {FirstName}";
        UserName = createUserDto.UserName;
      
        Email = createUserDto.Email;
        Phone = createUserDto.Phone;

    }
    public User(UpdateUserDto updateUserDto)
    {
        LastName = updateUserDto.LastName;
        FirstName = updateUserDto.FirstName;
        FullName = $"{LastName}, {FirstName}";
        UserName = updateUserDto.UserName;
        Email = updateUserDto.Email;
        Phone = updateUserDto.Phone;

    }
   
}



