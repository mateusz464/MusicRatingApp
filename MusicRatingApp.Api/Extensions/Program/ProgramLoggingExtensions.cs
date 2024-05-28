using Serilog;

namespace MusicRatingApp.Api.Extensions.Program;

public static class ProgramLoggingExtensions
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);
        builder.Services.AddLogging(b => b.AddConsole());
    }
}