namespace Server.Shared.Models;

public class BookmarkModel
{
    public string? UserId { get; set; }
    public string? RepositoryName { get; set; }
    public string? Description { get; set; }
    public string? HtmlUrl { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Language { get; set; }
    public User? User { get; set; }
}