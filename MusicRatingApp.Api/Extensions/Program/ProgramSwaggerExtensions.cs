using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;

namespace MusicRatingApp.Api.Extensions.Program;

public static class ProgramSwaggerExtensions
{
    [SuppressMessage("ReSharper", "InvertIf")]
    public static void AddSwaggerInDevelopment(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", GetOpenApiSecurityScheme());
            options.AddSecurityRequirement(GetOpenApiSecurityRequirement());
        });
    }
    
    private static OpenApiSecurityScheme GetOpenApiSecurityScheme()
    {
        return new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Bearer Authentication with JWT Token",
            Type = SecuritySchemeType.Http
        };
    }
    
    private static OpenApiSecurityRequirement GetOpenApiSecurityRequirement()
    {
        return new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        };
    }
}