using Microsoft.EntityFrameworkCore;
using MusicRatingApp.Api.Models.Database;

namespace MusicRatingApp.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public required DbSet<User> Users { get; set; }
    public required DbSet<AccountTier> AccountTiers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.AccountTier)
            .WithMany(a => a.Users)
            .HasForeignKey(u => u.AccountTierId);

        modelBuilder.Entity<AccountTier>()
            .HasMany(a => a.Users)
            .WithOne(u => u.AccountTier)
            .HasForeignKey(u => u.AccountTierId);

        base.OnModelCreating(modelBuilder);
    }
}