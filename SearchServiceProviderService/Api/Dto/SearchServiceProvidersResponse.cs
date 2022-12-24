using System.Text.Json.Serialization;

namespace SearchServiceProviderService.Api.Dto;

public class SearchServiceProvidersResponse
{
    [JsonPropertyName("totalHits")]
    public int TotalHits { get; set; }

    [JsonPropertyName("totalDocuments")]
    public int TotalDocuments { get; set; }

    [JsonPropertyName("results")]
    public List<SearchServiceProviderResponse> Results { get; set; } = new List<SearchServiceProviderResponse>();

}