using SpotifyAPI.Web;

namespace MusicRatingApp.Api.Endpoints.Spotify.Responses;

public class GetAlbumResponse
{
    public FullAlbum? Album { get; set; }
}