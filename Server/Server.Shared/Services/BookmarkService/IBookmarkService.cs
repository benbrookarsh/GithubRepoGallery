using Backend.Shared.Models;
using Server.Shared.Models;

namespace Server.Shared.Services.BookmarkService;

public interface IBookmarkService
{
    Task<BookmarkEntity?> DeleteBookmark(User user, BookmarkEntity repo);
    Task<BookmarkEntity?> AddBookmark(User user, BookmarkModel bookmark);
    Task<IEnumerable<BookmarkEntity>> GetBookmarks(User user);
}