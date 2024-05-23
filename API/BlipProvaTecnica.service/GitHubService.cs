using BlipProvaTecnica.models;
using BlipProvaTecnica.models.GitHubConfig;
using BlipProvaTecnica.service.Interfaces;
using System.Text.Json;

namespace BlipProvaTecnica.service
{
    public class GitHubService : IGitHubService
    {   

        private readonly HttpClient _httpClient;
        private readonly GitHubConfig _gitHubConfig;

        public GitHubService(HttpClient httpClient, GitHubConfig gitHubConfig)
        {
            _httpClient = httpClient;
            _gitHubConfig = gitHubConfig;
        }

        public async Task<BasicResponse>SearchRepositoriesAsync()
        {
            try
            {
                var basicResponse = new BasicResponse();

                var response = await _httpClient.GetAsync(_gitHubConfig.ApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    basicResponse.Status = "Success";
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    var repositories = await JsonSerializer.DeserializeAsync<List<RepositoriesAttributes>>(responseStream);

                    var filteredRepositories = FilterRepositories(repositories);

                    basicResponse.Data = filteredRepositories;

                }
                return basicResponse;

            }
            catch (Exception)
            {
                var basicResponse = new BasicResponse
                {
                    Status = "System Error",
                };
                
                return basicResponse;
            }
        }

        private List<RepositoriesAttributes> FilterRepositories(List<RepositoriesAttributes>? repositories)
        {

            var filteredRepo = repositories.Where(r => (r.Language is not null && r.Language.Equals("C#", StringComparison.OrdinalIgnoreCase))).Take(5).ToList();
            
            return filteredRepo;

        }
    }
}
