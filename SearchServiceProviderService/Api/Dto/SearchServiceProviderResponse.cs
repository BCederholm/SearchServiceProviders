using System.Text.Json.Serialization;

namespace SearchServiceProviderService.Api.Dto;

public class SearchServiceProviderResponse : SearchServiceProviderService.Domain.Models.ServiceProvider
{
    public SearchServiceProviderResponse(SearchServiceProviderService.Domain.Models.ServiceProvider baseClass)
    {
        Id = baseClass.Id;
        Name = baseClass.Name;
        Position = baseClass.Position;
    }

    [JsonPropertyName("distance")]
    [JsonPropertyOrder(order : 4)]
    public string Distance { get; set; } = String.Empty;

    [JsonPropertyName("score")]
    [JsonPropertyOrder(order : 5)]
    public int Score { get; set; }
}