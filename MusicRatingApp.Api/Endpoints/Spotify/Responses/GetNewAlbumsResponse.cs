using SpotifyAPI.Web;

namespace MusicRatingApp.Api.Endpoints.Spotify.Responses;

public class GetNewAlbumsResponse
{
    public NewReleasesResponse? NewAlbums { get; set; }
}