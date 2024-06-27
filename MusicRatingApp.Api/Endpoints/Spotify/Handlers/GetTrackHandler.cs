using LanguageExt.Common;
using Mediator;
using MusicRatingApp.Api.Endpoints.Spotify.Requests;
using MusicRatingApp.Api.Endpoints.Spotify.Responses;
using MusicRatingApp.Api.Services.Spotify;

namespace MusicRatingApp.Api.Endpoints.Spotify.Handlers;

public class GetTrackHandler : IRequestHandler<GetTrackRequest, Result<GetTrackResponse>>
{
    private readonly SpotifyService _spotifyService;
    
    public GetTrackHandler(SpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }

    public async ValueTask<Result<GetTrackResponse>> Handle(GetTrackRequest request, CancellationToken cancellationToken)
    {
        var track = await _spotifyService.SpotifyClient.Tracks.Get(request.TrackId, cancellationToken);
        return new GetTrackResponse
        {
            Track = track
        };
    }
}