using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicRatingApp.Api.Models.Database;

public class RefreshToken
{
    public int Id { get; set; }

    [Required]
    public required string Token { get; set; }

    public DateTime Expires { get; set; }

    [NotMapped]
    public bool IsExpired => DateTime.UtcNow >= Expires;

    public DateTime Created { get; set; }

    public DateTime? Revoked { get; set; }

    public string? ReplacedByToken { get; set; }

    [NotMapped]
    public bool IsActive => Revoked == null && !IsExpired;
    
    public required int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}