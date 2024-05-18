namespace MusicRatingApp.Api.Models.Database;

public class AccountTier
{
    public int Id { get; set; }
    public required string TierName { get; set; }
    public string? Description { get; set; }
    public List<User>? Users { get; set; }
}