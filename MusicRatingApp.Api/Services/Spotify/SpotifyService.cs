using SpotifyAPI.Web;

namespace MusicRatingApp.Api.Services.Spotify;

public class SpotifyService(IConfiguration configuration)
{
    public SpotifyClient SpotifyClient { get; private set; } = null!;
    private readonly SpotifyClientConfig _config = SpotifyClientConfig.CreateDefault();

    public async Task InitializeClientAsync()
    {
        var clientId = configuration["Variables:SpotifyClientID"];
        var clientSecret = configuration["Variables:SpotifyClientSecret"];
        
        if (clientId is not null && clientSecret is not null)
        {
            var request = new ClientCredentialsRequest(clientId, clientSecret);
            var response = await new OAuthClient(_config).RequestToken(request);
            SpotifyClient = new SpotifyClient(_config.WithToken(response.AccessToken));
        }
    }

    public async Task RefreshClientAsync()
    {
        await InitializeClientAsync();
    }
}