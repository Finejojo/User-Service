using User_Service.Domain.AppUser.Models;

namespace UserMicroservices.Services.Interface
{
    public interface IJwtTokenGeneratorService
    {
        string GenerateToken(User user);
    }

}
