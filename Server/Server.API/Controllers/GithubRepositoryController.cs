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
    public async Task<IActionResult> Search(string search)
    {
        var response = await  _githubRepositoryService.Search(search);
        
        return Ok(response);
    }
}