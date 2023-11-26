
using MongoDB.Driver;
namespace UserMicroservices.Services.Interface;
public interface IPasswordProvider
{
    (string passwordHash, byte[] passwordSalt) CreatePasswordHash(string password);
    bool VerifyPasswordHash(string password, string storedHash, byte[] storedSalt);
}
