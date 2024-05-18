using System.IdentityModel.Tokens.Jwt;
using MusicRatingApp.Api.Models.Database;

namespace MusicRatingApp.Api.Services.Token;

public interface IAuthService
{
    public Task<(string JwtToken, string RefreshToken)?> GenerateNewTokensAsync(JwtSecurityToken token, string refreshToken);
    public (string token, DateTime expires) CreateJwtToken(int userId);
    public RefreshToken CreateRefreshToken(int userId);
}