namespace MusicRatingApp.Api.Models.Database;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? Name { get; set; }
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
    public int? AccountTierId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public AccountTier? AccountTier { get; set; }
}