using Mediator;
using MusicRatingApp.Api.Endpoints.Spotify.Requests;

namespace MusicRatingApp.Api.Endpoints.Spotify;

public class SpotifyEndpoints : IEndpointGroup
{
    public void RouteGroup(RouteGroupBuilder endpointGroup)
    {
        var spotifyGroup = endpointGroup.MapGroup("/spotify")
            .RequireAuthorization();

        spotifyGroup.MapGet("/track/{trackId}", GetTrack);
        spotifyGroup.MapGet("/album/{albumId}", GetAlbum);
    }

    private static async Task<IResult> GetTrack(HttpContext context, IMediator mediator, string trackId)
    {
        var request = new GetTrackRequest { TrackId = trackId };
        var response = await mediator.Send(request);
        return response.Match(res => res.Track is not null
            ? Results.Ok((object?)res.Track)
            : Results.NotFound(), Results.BadRequest);
    }
    
    private static async Task<IResult> GetAlbum(HttpContext context, IMediator mediator, string albumId)
    {
        var request = new GetAlbumRequest { AlbumId = albumId };
        var response = await mediator.Send(request);
        return response.Match(res => res.Album is not null
            ? Results.Ok((object?)res.Album)
            : Results.NotFound(), Results.BadRequest);
    }
}