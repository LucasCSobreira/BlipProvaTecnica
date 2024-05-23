using BlipProvaTecnica.models;
using BlipProvaTecnica.service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlipProvaTecnica.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;

        public GitHubController(IGitHubService GitHubService)
        {
            _gitHubService = GitHubService;
        }

        [Route("repositories")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<BasicResponse> GetRepositoriesAsync()
        {
            var response = await _gitHubService.SearchRepositoriesAsync();

            if (response.Status.Equals("Success", StringComparison.OrdinalIgnoreCase))
            {
                if (response.Data.Any())
                {
                    Response.StatusCode = 200;
                    return response;
                }
                else
                {
                    Response.StatusCode = 204;
                    return response;
                }
            }

            Response.StatusCode = 500;
            return response;
        }
    }
}
