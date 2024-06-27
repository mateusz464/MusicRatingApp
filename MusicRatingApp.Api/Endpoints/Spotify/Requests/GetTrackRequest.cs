using LanguageExt.Common;
using Mediator;
using MusicRatingApp.Api.Endpoints.Spotify.Responses;

namespace MusicRatingApp.Api.Endpoints.Spotify.Requests;

public class GetTrackRequest : IRequest<Result<GetTrackResponse>>
{
    public required string TrackId { get; set; }
}