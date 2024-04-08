using Microsoft.EntityFrameworkCore;
using Server.Shared.Extensions;
using Server.Shared.Interfaces;
using Server.Shared.Models;

namespace Server.Shared.Services.BookmarkService;

public class BookmarkService : IBookmarkService
{
    private readonly IRepository<BookmarkEntity> _bookmarkRepository;

    public BookmarkService(IRepository<BookmarkEntity> bookmarkRepository)
    {
        _bookmarkRepository = bookmarkRepository;
    }

    public async Task<BookmarkEntity?> AddBookmark(User user, BookmarkModel bookmark)
    {
        bookmark.User = user;
        return await _bookmarkRepository.InsertAsync(bookmark.ToEntity());
    }

    public async Task<BookmarkEntity?> DeleteBookmark(User user, BookmarkEntity repo) => 
        await _bookmarkRepository.DeleteAsync(repo);

    public async Task<IEnumerable<BookmarkEntity>> GetBookmarks(User user) => 
        await _bookmarkRepository.TableNoTracking.Where(a => a.UserId == user.Id)
            .ToListAsync();
}