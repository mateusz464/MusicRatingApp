using SpotifyAPI.Web;

namespace MusicRatingApp.Api.Endpoints.Spotify.Responses;

public class GetTrackResponse
{
    public FullTrack? Track { get; set; }
}