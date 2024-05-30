namespace MusicRatingApp.Api.Services.Spotify;

public class SpotifyClientRefreshService(SpotifyService spotifyClient, ILogger<SpotifyClientRefreshService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await spotifyClient.RefreshClientAsync();
                logger.LogInformation("Spotify client refreshed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error refreshing Spotify client");
            }

            await Task.Delay(TimeSpan.FromMinutes(50), stoppingToken);
        }
    }
}