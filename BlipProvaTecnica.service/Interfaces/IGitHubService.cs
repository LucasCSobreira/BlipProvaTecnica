using BlipProvaTecnica.models;

namespace BlipProvaTecnica.service.Interfaces
{
    public interface IGitHubService
    {
        public Task<BasicResponse> SearchRepositoriesAsync();
    }
}
