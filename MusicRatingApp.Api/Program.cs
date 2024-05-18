using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MusicRatingApp.Api.Data;
using MusicRatingApp.Api.Endpoints;
using MusicRatingApp.Api.Extensions.Program;
using MusicRatingApp.Api.Services.Token;

#region Builder

var builder = WebApplication.CreateBuilder(args);
builder.AddSerilog();

builder.Services.AddSwagger();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddHttpContextAccessor();

var connectionString = builder.Configuration["Variables:ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

#endregion

#region Services

builder.Services.AddScoped<IAuthService, AuthService>();

#endregion

#region App

var app = builder.Build();
app.AddSwaggerInDevelopment();
app.UseHttpsRedirection();

app.RegisterEndpoints();

app.Run();

#endregion