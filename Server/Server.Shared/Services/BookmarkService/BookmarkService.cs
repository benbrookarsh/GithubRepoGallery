using Backend.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Server.Shared.Interfaces;
using Server.Shared.Models;

namespace Server.Shared.Services.BookmarkService;

public class BookmarkService : IBookmarkService
{
    private readonly IRepository<BookmarkEntity?> _bookmarkRepository;

    public BookmarkService(IRepository<BookmarkEntity?> bookmarkRepository)
    {
        _bookmarkRepository = bookmarkRepository;
    }

    public async Task<BookmarkEntity?> AddBookmark(User user, GitHubRepo repo)
    {
        var bookmark = new BookmarkEntity
        {
            UserId = user.Id,
            RepositoryName = repo.Name,
            Description = repo.Description,
            HtmlUrl = repo.Html_Url,
            AvatarUrl = repo.Owner.Avatar_Url,
            Language = repo.Language,
            User = user
        };

        return await _bookmarkRepository.InsertAsync(bookmark);
    }

    public async Task<BookmarkEntity?> DeleteBookmark(User user, BookmarkEntity repo) => 
        await _bookmarkRepository.DeleteAsync(repo);

    public async Task<IEnumerable<BookmarkEntity>> GetBookmarks(User user) => 
        await _bookmarkRepository.TableNoTracking.Where(a => a.UserId == user.Id)
            .ToListAsync();
}