using Newtonsoft.Json;

namespace DogApiTests.Models;

/// <summary>
/// Resposta com uma única imagem
/// </summary>
public class SingleImageResponse
{
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Resposta com múltiplas imagens
/// </summary>
public class MultipleImagesResponse
{
    [JsonProperty("message")]
    public List<string> Message { get; set; } = new();

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Resposta com lista de raças (breed -> sub-breeds)
/// </summary>
public class AllBreedsResponse
{
    [JsonProperty("message")]
    public Dictionary<string, List<string>> Message { get; set; } = new();

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Resposta com lista de sub-raças ou raças simples
/// </summary>
public class StringListResponse
{
    [JsonProperty("message")]
    public List<string> Message { get; set; } = new();

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;
}
