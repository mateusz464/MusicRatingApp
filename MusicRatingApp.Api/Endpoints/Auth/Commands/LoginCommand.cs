using LanguageExt.Common;
using Mediator;

namespace MusicRatingApp.Api.Endpoints.Auth.Commands;

public class LoginCommand : IRequest<Result<LoginResponse>>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}