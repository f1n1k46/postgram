using Postgram.Domain.Models;

namespace Postgram.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(User user);
    }
}
