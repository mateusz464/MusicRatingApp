using Microsoft.EntityFrameworkCore;
using MusicRatingApp.Api.Models.Database;

namespace MusicRatingApp.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public required DbSet<User> Users { get; set; }
    public required DbSet<AccountTier> AccountTiers { get; set; }
    public required DbSet<RefreshToken> RefreshTokens { get; set; }
}