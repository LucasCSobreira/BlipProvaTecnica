using System.Text.Json.Serialization;

namespace BlipProvaTecnica.models
{
    public class RepositoriesAttributes
    {
        [JsonPropertyName("full_name")]
        public string CompleteName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("language")]
        public string? Language { get; set; }

        [JsonPropertyName("created_at")]
        public string CreateDate { get; set; }

    }
    
}
