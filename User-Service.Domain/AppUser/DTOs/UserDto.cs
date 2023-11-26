
using MongoDB.Bson.Serialization.Attributes;


public class UserDto
{
    public string Reference { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

