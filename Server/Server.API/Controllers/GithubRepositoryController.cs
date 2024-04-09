namespace Server.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class GithubRepositoryController : BaseController
{
    private readonly IGithubRepositoryService _githubRepositoryService;

    public GithubRepositoryController(IGithubRepositoryService githubRepositoryService)
    {
        _githubRepositoryService = githubRepositoryService;
    }

    [HttpGet]
    public Task<ServerResponse<GitHubRepoResult?>> Search(string search)
    {
        return _githubRepositoryService.Search(search).ToServerResponseAsync();
    }
}