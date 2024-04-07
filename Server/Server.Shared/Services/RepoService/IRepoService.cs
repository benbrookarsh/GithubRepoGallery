using Backend.Shared.Models;

namespace Server.Shared.Services.RepoService;

public interface IRepoService
{
    Task<GitHubRepoResult?> Search(string search);
}