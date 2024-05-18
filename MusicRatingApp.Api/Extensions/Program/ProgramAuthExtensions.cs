using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MusicRatingApp.Api.Services.Token;

namespace MusicRatingApp.Api.Extensions.Program;

public static class ProgramAuthExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration["Variables:JwtSigningKey"] ??
                                          throw new InvalidOperationException(
                                              "JwtSigningKey is not set in appsettings.json"));

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = false, // Lifetime is validated in OnTokenValidated
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Variables:JwtIssuer"] ??
                                  throw new InvalidOperationException("JwtIssuer is not set in appsettings.json"),
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.FromSeconds(5)
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Cookies["access_token"];

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadToken(context.SecurityToken.ToString()) as JwtSecurityToken;
                        var expiration = jwtToken?.ValidTo;

                        if (!expiration.HasValue)
                        {
                            context.Fail("Token has no expiration date.");
                            return;
                        }

                        if (expiration.Value >= DateTime.UtcNow) // Token is valid
                        {
                            return;
                        }

                        var refreshToken = context.Request.Cookies["refresh_token"];
                        if (!string.IsNullOrEmpty(refreshToken))
                        {
                            var tokenService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();

                            var newTokens =
                                await tokenService.GenerateNewTokensAsync(jwtToken!, refreshToken);

                            if (newTokens is null)
                            {
                                context.Fail("Failed to generate new tokens.");
                                return;
                            }

                            var newAccessToken = newTokens.Value.JwtToken;
                            var newRefreshToken = newTokens.Value.RefreshToken;
                            var cookieOptions = new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = true,
                                SameSite = SameSiteMode.Strict
                            };

                            context.Response.Cookies.Append("access_token", newAccessToken, cookieOptions);
                            context.Response.Cookies.Append("refresh_token", newRefreshToken, cookieOptions);
                        }
                    }
                };
            });

        return services;
    }
}