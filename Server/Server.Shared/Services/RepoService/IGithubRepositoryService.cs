using Backend.Shared.Models;

namespace Server.Shared.Services.RepoService;

public interface IGithubRepositoryService
{
    Task<GitHubRepoResult?> Search(string search);
}