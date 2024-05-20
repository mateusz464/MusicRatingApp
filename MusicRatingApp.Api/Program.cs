using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MusicRatingApp.Api.Data;
using MusicRatingApp.Api.Endpoints;
using MusicRatingApp.Api.Extensions.Program;
using MusicRatingApp.Api.Services.Auth;

#region Builder

var builder = WebApplication.CreateBuilder(args);

// Extension methods
builder.AddJwtAuthentication();
builder.AddSerilog();
builder.Services.AddSwagger();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddHttpContextAccessor();

// DbContext
var connectionString = builder.Configuration["Variables:ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add Mediator
builder.Services.AddMediator(options =>
{
    options.Namespace = "MusicRatingApp.Api";
    options.ServiceLifetime = ServiceLifetime.Transient;
});

#endregion

#region Services

builder.Services.AddScoped<IAuthService, AuthService>();

#endregion

#region App

var app = builder.Build();
app.AddSwaggerInDevelopment();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.RegisterEndpoints();

app.MapGet("/authed", () => "Hello mr authed user")
    .RequireAuthorization();

app.Run();

#endregion