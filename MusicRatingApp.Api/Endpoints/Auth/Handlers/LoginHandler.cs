using LanguageExt.Common;
using Mediator;
using MusicRatingApp.Api.Endpoints.Auth.Commands;
using Microsoft.EntityFrameworkCore;
using MusicRatingApp.Api.Data;
using MusicRatingApp.Api.Endpoints.Auth.Responses;
using MusicRatingApp.Api.Services.Auth;

namespace MusicRatingApp.Api.Endpoints.Auth.Handlers;

public class LoginHandler : IRequestHandler<LoginRequest, Result<LoginResponse>>
{
    private readonly IAuthService _authService;
    private readonly AppDbContext _dbContext;

    public LoginHandler(IAuthService authService, AppDbContext dbContext)
    {
        _authService = authService;
        _dbContext = dbContext;
    }

    public async ValueTask<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var email = request.Email;
        var password = request.Password;
        
        // Authenticate user
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken: cancellationToken);
        if (user is null)
        {
            return new Result<LoginResponse>(new Exception("Invalid email or password"));
        }
        
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return new Result<LoginResponse>(new Exception("Invalid email or password"));
        }
        
        // Generate tokens
        var jwtToken = _authService.CreateJwtToken(user.Id);
        var refreshToken = _authService.CreateRefreshToken(user.Id);
        
        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new Result<LoginResponse>(new LoginResponse
        {
            JwtToken = jwtToken,
            RefreshToken = refreshToken.Token
        });
    }
}