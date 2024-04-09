namespace Server.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookmarkController : BaseController
{
    private readonly IBookmarkService _bookmarkService;

    public BookmarkController(IBookmarkService bookmarkService)
    {
        _bookmarkService = bookmarkService;
    }

    [HttpPost]
    public Task<BookmarkEntity?> AddBookmark([FromBody] BookmarkModel repo) => 
        _bookmarkService.AddBookmark(User, repo);

    [HttpDelete]
    public Task<BookmarkEntity> DeleteBookmark([FromBody] long bookmarkId) =>
        _bookmarkService.DeleteBookmark(User, bookmarkId);

    [HttpGet]
    public async Task<IEnumerable<BookmarkEntity>> GetBookmarks() =>
        await _bookmarkService.GetBookmarks(User);
}