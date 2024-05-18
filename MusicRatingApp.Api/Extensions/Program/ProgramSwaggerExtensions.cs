using System.Diagnostics.CodeAnalysis;

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
        services.AddSwaggerGen();
    }
}