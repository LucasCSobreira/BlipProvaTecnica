

namespace BlipProvaTecnica.models
{
    public class BasicResponse
    {
        public BasicResponse()
        {
                
        }

        public string Status { get; set; }

        //public GitHubRepositories? Data { get; set; }
        public List<RepositoriesAttributes> Data { get; set; }

    }
}
