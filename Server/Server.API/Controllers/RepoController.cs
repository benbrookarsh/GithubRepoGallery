namespace Server.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class RepoController : BaseController
{
    private readonly IRepoService _repoService;

    public RepoController(IRepoService repoService)
    {
        _repoService = repoService;
    }

    [HttpGet("search")]
    public async Task<ServerResponse<GitHubRepoResult?>> Search(string search)
    {
        var user = GetCurrentUser();
        Console.WriteLine(user);
        return await _repoService.Search(search).ToServerResponseAsync();
    }
}