using Server.Shared.Models;

namespace Server.Shared.Extensions;

public static class EntityModelExtensions
{
    public static BookmarkEntity ToEntity(this BookmarkModel model) =>
        new()
        {
            Id = default,
            UserId = model.UserId ?? "",
            RepositoryName = model.RepositoryName ?? "",
            Description = model.Description ?? "",
            HtmlUrl = model.HtmlUrl ?? "",
            AvatarUrl = model.AvatarUrl ?? "",
            Language = model.Language ?? "", 
            User = model.User
        };
}