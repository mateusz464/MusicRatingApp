using LanguageExt.Common;
using Mediator;
using MusicRatingApp.Api.Endpoints.Spotify.Requests;
using MusicRatingApp.Api.Endpoints.Spotify.Responses;
using MusicRatingApp.Api.Services.Spotify;

namespace MusicRatingApp.Api.Endpoints.Spotify.Handlers;

public class GetAlbumHandler: IRequestHandler<GetAlbumRequest, Result<GetAlbumResponse>>
{
    private readonly SpotifyService _spotifyService;
    
    public GetAlbumHandler(SpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }
    
    public async ValueTask<Result<GetAlbumResponse>> Handle(GetAlbumRequest request, CancellationToken cancellationToken)
    {
        var album = await _spotifyService.SpotifyClient.Albums.Get(request.AlbumId, cancellationToken);
        return new GetAlbumResponse
        {
            Album = album
        };
    }
}