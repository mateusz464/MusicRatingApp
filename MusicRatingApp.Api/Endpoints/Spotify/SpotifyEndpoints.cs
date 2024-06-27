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
    }

    private static async Task<IResult> GetTrack(HttpContext context, IMediator mediator, string trackId)
    {
        var request = new GetTrackRequest { TrackId = trackId };
        var response = await mediator.Send(request);
        return response.Match(Results.Ok, Results.BadRequest);
    }
}