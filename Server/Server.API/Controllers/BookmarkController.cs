namespace Server.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class BookmarkController : BaseController
{
    private readonly IBookmarkService _bookmarkService;

    public BookmarkController(IBookmarkService bookmarkService)
    {
        _bookmarkService = bookmarkService;
    }

    [HttpPost]
    public Task<ServerResponse<BookmarkEntity?>> AddBookmark([FromBody] BookmarkModel repo)
    {
        return _bookmarkService.AddBookmark(User, repo).ToServerResponseAsync();
    }
    
    [HttpDelete]
    public Task<ServerResponse<BookmarkEntity>> DeleteBookmark([FromBody] long bookmarkId) =>
        _bookmarkService.DeleteBookmark(User, bookmarkId)
            .ToServerResponseAsync();

    [HttpGet]
    public async Task<ServerResponse<IEnumerable<BookmarkEntity>>> GetBookmarks() =>
        await _bookmarkService.GetBookmarks(User)
            .ToServerResponseAsync();
}