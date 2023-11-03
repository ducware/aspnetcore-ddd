using DDD.Domain.Entities;

namespace DDD.Application.Services.Authentication
{
    public record AuthenticationResult(
        User User,
        string Token
    );
}
