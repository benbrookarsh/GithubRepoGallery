using System.Net.Http.Headers;
using System.Net.Http.Json;
using Backend.Shared.Models;

namespace Server.Shared.Services.RepoService;

public class GithubGithubRepositoryService : IGithubRepositoryService
{
    private readonly HttpClient _httpClient;

    public GithubGithubRepositoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("ASP.NET", "11.0"));
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
    }
    
    public async Task<GitHubRepoResult?> Search(string search)
    {
        var url = new Uri($"https://api.github.com/search/repositories?q={search}");
        
        var response = await _httpClient.GetAsync(url);
        
        return await response.Content.ReadFromJsonAsync<GitHubRepoResult>();
    }  
}