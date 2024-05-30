using System.Data.Common;
using LanguageExt.Common;
using Mediator;
using MusicRatingApp.Api.Data;
using MusicRatingApp.Api.Endpoints.Auth.Commands;
using MusicRatingApp.Api.Endpoints.Auth.Responses;
using MusicRatingApp.Api.Models.Database;
using MusicRatingApp.Api.Services.Auth;

namespace MusicRatingApp.Api.Endpoints.Auth.Handlers;

public class RegisterHandler : IRequestHandler<RegisterRequest, Result<RegisterResponse>>
{
    private readonly IAuthService _authService;
    private readonly AppDbContext _dbContext;

    public RegisterHandler(IAuthService authService, AppDbContext dbContext)
    {
        _authService = authService;
        _dbContext = dbContext;
    }

    public async ValueTask<Result<RegisterResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        var newUser = new User
        {
            Username = request.Username!,
            PasswordHash = passwordHash,
            Email = request.Email!,
            Avatar = request.AvatarUrl
        };
        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var jwtToken = _authService.CreateJwtToken(newUser.Id);
        var refreshToken = _authService.CreateRefreshToken(newUser.Id);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Result<RegisterResponse>(new RegisterResponse
        {
            JwtToken = jwtToken,
            RefreshToken = refreshToken.Token
        });
    }
}