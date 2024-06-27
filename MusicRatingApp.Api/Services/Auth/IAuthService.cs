using MusicRatingApp.Api.Models.Database;

namespace MusicRatingApp.Api.Services.Auth;

public interface IAuthService
{
    public Task<(string JwtToken, string RefreshToken)?> GenerateNewTokensAsync(string refreshToken);
    public string CreateJwtToken(int userId);
    public RefreshToken CreateRefreshToken(int userId);
}