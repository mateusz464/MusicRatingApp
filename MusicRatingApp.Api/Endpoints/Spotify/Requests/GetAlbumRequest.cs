using LanguageExt.Common;
using Mediator;
using MusicRatingApp.Api.Endpoints.Spotify.Responses;

namespace MusicRatingApp.Api.Endpoints.Spotify.Requests;

public class GetAlbumRequest: IRequest<Result<GetAlbumResponse>>
{
    public required string AlbumId { get; set; }
}