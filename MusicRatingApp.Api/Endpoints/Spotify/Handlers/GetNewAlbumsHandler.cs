using LanguageExt.Common;
using Mediator;
using MusicRatingApp.Api.Endpoints.Spotify.Requests;
using MusicRatingApp.Api.Endpoints.Spotify.Responses;
using MusicRatingApp.Api.Services.Spotify;

namespace MusicRatingApp.Api.Endpoints.Spotify.Handlers;

public class GetNewAlbumsHandler : IRequestHandler<GetNewAlbumsRequest, Result<GetNewAlbumsResponse>>
{
    private readonly SpotifyService _spotifyService;
    
    public GetNewAlbumsHandler(SpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }
    
    public async ValueTask<Result<GetNewAlbumsResponse>> Handle(GetNewAlbumsRequest request, CancellationToken cancellationToken)
    {
        var newAlbums = await _spotifyService.SpotifyClient.Browse.GetNewReleases(cancellationToken);
        return new GetNewAlbumsResponse
        {
            NewAlbums = newAlbums
        };
    }
}