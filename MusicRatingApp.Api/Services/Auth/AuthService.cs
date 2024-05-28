using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicRatingApp.Api.Data;
using MusicRatingApp.Api.Models.Database;

namespace MusicRatingApp.Api.Services.Auth;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<(string JwtToken, string RefreshToken)?> GenerateNewTokensAsync(string oldRefreshTokenString)
    {
        var oldRefreshToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == oldRefreshTokenString);

        if (oldRefreshToken?.UserId is null)
        {
            return null;
        }

        var userId = oldRefreshToken.UserId;

        // Generate new Jwt and refresh tokens
        var createJwtTokenResponse = CreateJwtToken(userId);
        var refreshToken = CreateRefreshToken(userId);

        // Update the database
        _dbContext.RefreshTokens.Add(refreshToken);
        
        oldRefreshToken.ReplacedByToken = refreshToken.Token;
        oldRefreshToken.Revoked = DateTime.Now;
        
        await _dbContext.SaveChangesAsync();

        return (createJwtTokenResponse, refreshToken.Token);
    }

    public string CreateJwtToken(int userId)
    {
        var expiresAt = DateTime.Now.AddHours(1);

        var tokenClaims = new List<Claim>
        {
            new("user_id", userId.ToString())
        };

        var jwtSigningKey = _configuration["Variables:JwtSigningKey"] ??
                            throw new InvalidOperationException("JwtSigningKey not found in appsettings.json");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSigningKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var issuer = _configuration["Variables:JwtIssuer"];

        var token = new JwtSecurityToken(claims: tokenClaims,
            notBefore: DateTime.Now,
            expires: expiresAt,
            signingCredentials: credentials,
            issuer: issuer
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    public RefreshToken CreateRefreshToken(int userId)
    {
        var refreshToken = new RefreshToken()
        {
            Token = Guid.NewGuid().ToString(),
            Expires = DateTime.Now.AddDays(7),
            Created = DateTime.Now,
            UserId = userId
        };

        return refreshToken;
    }
}