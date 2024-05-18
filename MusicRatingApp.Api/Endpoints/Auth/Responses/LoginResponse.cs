namespace MusicRatingApp.Api.Endpoints.Auth.Commands;

public class LoginResponse
{
    public required string JwtToken { get; set; }
    public required string RefreshToken { get; set; }
}