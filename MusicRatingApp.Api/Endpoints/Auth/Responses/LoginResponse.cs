namespace MusicRatingApp.Api.Endpoints.Auth.Responses;

public class LoginResponse
{
    public required string JwtToken { get; set; }
    public required string RefreshToken { get; set; }
}