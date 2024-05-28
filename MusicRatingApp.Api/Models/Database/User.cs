using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicRatingApp.Api.Models.Database;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Username { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Email { get; set; }

    [MaxLength(255)]
    public string? PasswordHash { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; }

    public string? Bio { get; set; }

    [MaxLength(255)]
    public string? Avatar { get; set; }

    public int? AccountTierId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(AccountTierId))]
    public AccountTier? AccountTier { get; set; }
}