namespace Server.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GithubRepositoryController : BaseController
{
    private readonly IGithubRepositoryService _githubRepositoryService;

    public GithubRepositoryController(IGithubRepositoryService githubRepositoryService)
    {
        _githubRepositoryService = githubRepositoryService;
    }

    [HttpGet]
    public Task<GitHubRepoResult?> Search(string search) => _githubRepositoryService.Search(search);
}