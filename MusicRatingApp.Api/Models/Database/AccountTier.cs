using System.ComponentModel.DataAnnotations;

namespace MusicRatingApp.Api.Models.Database;

public class AccountTier
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public required string TierName { get; set; }

    public string? Description { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}