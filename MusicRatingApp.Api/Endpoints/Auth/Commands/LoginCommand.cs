using LanguageExt.Common;
using Mediator;
using MusicRatingApp.Api.Endpoints.Auth.Responses;

namespace MusicRatingApp.Api.Endpoints.Auth.Commands;

public class LoginCommand : IRequest<Result<LoginResponse>>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}