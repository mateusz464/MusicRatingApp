using Mediator;
using MusicRatingApp.Api.Endpoints.Auth.Commands;

namespace MusicRatingApp.Api.Endpoints.Auth;

public class AuthEndpoints : IEndpointGroup
{
    public void RouteGroup(RouteGroupBuilder endpointGroup)
    {
        var authGroup = endpointGroup.MapGroup("/auth");

        authGroup.MapPost("/login", Login)
            .AddEndpointFilter<ValidationFilter<LoginCommand>>()
            .AllowAnonymous();

        authGroup.MapPost("/register", Register);
    }

    private static async Task<IResult> Register(IMediator mediator)
    {
        throw new NotImplementedException();
    }

    private static async Task<IResult> Login(IMediator mediator, LoginCommand loginCommand)
    {
        var result = await mediator.Send(loginCommand);
        return result.Match(Results.Ok, Results.BadRequest);
    }
}