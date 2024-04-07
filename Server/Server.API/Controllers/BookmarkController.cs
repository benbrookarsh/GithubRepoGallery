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

    [HttpPost("add")]
    public async Task<ServerResponse<BookmarkEntity?>> AddBookmark([FromBody] GitHubRepo repo)
    {
        var user = GetCurrentUser();
        return await _bookmarkService.AddBookmark(user, repo)
            .ToServerResponseAsync();
    }
    
    [HttpPost("delete")]
    public async Task<ServerResponse<BookmarkEntity?>> DeleteBookmark([FromBody] BookmarkEntity repo)
    {
        var user = GetCurrentUser();
        return await _bookmarkService.DeleteBookmark(user, repo)
            .ToServerResponseAsync();
    }
    
    [HttpGet]
    public async Task<ServerResponse<IEnumerable<BookmarkEntity>>> GetBookmarks() =>
        await _bookmarkService.GetBookmarks(GetCurrentUser())
            .ToServerResponseAsync();
}