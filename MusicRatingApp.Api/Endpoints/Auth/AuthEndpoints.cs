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

        authGroup.MapPost("/register", Register)
            .AddEndpointFilter<ValidationFilter<RegisterCommand>>()
            .AllowAnonymous();
    }

    private static async Task<IResult> Register(HttpContext httpContext, IMediator mediator, RegisterCommand request)
    {
        var result = await mediator.Send(request);
        return result.Match(Succ: res =>
        {
            httpContext.Response.Cookies.Append("access_token", res.JwtToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });
            httpContext.Response.Cookies.Append("refresh_token", res.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });
            return Results.Ok(res);
        }, Results.BadRequest);
    }

    private static async Task<IResult> Login(HttpContext httpContext, IMediator mediator, LoginCommand request)
    {
        var result = await mediator.Send(request);
        return result.Match(Succ: res =>
        {
            httpContext.Response.Cookies.Append("access_token", res.JwtToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });
            httpContext.Response.Cookies.Append("refresh_token", res.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });
            return Results.Ok(res);
        }, Results.BadRequest);
    }
}