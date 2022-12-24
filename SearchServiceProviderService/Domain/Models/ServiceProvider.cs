using System.Text.Json.Serialization;

namespace SearchServiceProviderService.Domain.Models;

public class ServiceProvider
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("position")]
    public Position? Position { get; set; }
}