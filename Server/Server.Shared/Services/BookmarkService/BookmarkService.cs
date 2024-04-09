using Microsoft.EntityFrameworkCore;
using Server.Shared.Exceptions;
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

    public async Task<BookmarkEntity> DeleteBookmark(User user, long bookmarkId)
    {
        var bookmarkEntity = await _bookmarkRepository
            .TableNoTracking.FirstOrDefaultAsync(x => x.Id == bookmarkId && x.UserId == user.Id);
        
        if (bookmarkEntity is null) 
            throw new BookMarkNotFoundException($"Bookmark not found for user {user.UserName}");
        
        var deleteSuccess = await _bookmarkRepository.DeleteAsync(bookmarkEntity);
        
        if (deleteSuccess is null) 
            throw new BookMarkNotFoundException($"Failed to delete bookmark for user {user.UserName}");

        return deleteSuccess;
    }

    public async Task<IEnumerable<BookmarkEntity>> GetBookmarks(User user) => 
        await _bookmarkRepository.TableNoTracking.Where(a => a.UserId == user.Id)
            .ToListAsync();
    
}