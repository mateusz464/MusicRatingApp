using LanguageExt.Common;
using Mediator;
using MusicRatingApp.Api.Endpoints.Auth.Responses;

namespace MusicRatingApp.Api.Endpoints.Auth.Commands;

public class RegisterCommand : IRequest<Result<RegisterResponse>>
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? AvatarUrl { get; set; }
}