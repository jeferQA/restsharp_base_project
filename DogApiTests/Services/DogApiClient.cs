using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using DogApiTests.Models;

namespace DogApiTests.Services;

/// <summary>
/// Cliente HTTP para a Dog CEO API, encapsula todas as chamadas com RestSharp.
/// </summary>
public class DogApiClient : IDisposable
{
    private readonly RestClient _client;
    private const string BaseUrl = "https://dog.ceo/api";

    public DogApiClient()
    {
        var options = new RestClientOptions(BaseUrl)
        {
            ThrowOnAnyError = false,
            MaxTimeout = 30_000
        };

        _client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
    }

    // ──────────────────────────────────────────────────────────────
    // Breeds
    // ──────────────────────────────────────────────────────────────

    /// <summary>GET /breeds/list/all — lista todas as raças e sub-raças</summary>
    public async Task<RestResponse<AllBreedsResponse>> GetAllBreedsAsync()
    {
        var request = new RestRequest("breeds/list/all");
        return await _client.ExecuteGetAsync<AllBreedsResponse>(request);
    }

    // ──────────────────────────────────────────────────────────────
    // Random images
    // ──────────────────────────────────────────────────────────────

    /// <summary>GET /breeds/image/random — imagem aleatória de qualquer raça</summary>
    public async Task<RestResponse<SingleImageResponse>> GetRandomImageAsync()
    {
        var request = new RestRequest("breeds/image/random");
        return await _client.ExecuteGetAsync<SingleImageResponse>(request);
    }

    /// <summary>GET /breeds/image/random/{count} — múltiplas imagens aleatórias</summary>
    public async Task<RestResponse<MultipleImagesResponse>> GetMultipleRandomImagesAsync(int count)
    {
        var request = new RestRequest($"breeds/image/random/{count}");
        return await _client.ExecuteGetAsync<MultipleImagesResponse>(request);
    }

    // ──────────────────────────────────────────────────────────────
    // By breed
    // ──────────────────────────────────────────────────────────────

    /// <summary>GET /breed/{breed}/images — todas as imagens de uma raça</summary>
    public async Task<RestResponse<MultipleImagesResponse>> GetBreedImagesAsync(string breed)
    {
        var request = new RestRequest($"breed/{breed}/images");
        return await _client.ExecuteGetAsync<MultipleImagesResponse>(request);
    }

    /// <summary>GET /breed/{breed}/images/random — imagem aleatória de uma raça</summary>
    public async Task<RestResponse<SingleImageResponse>> GetRandomBreedImageAsync(string breed)
    {
        var request = new RestRequest($"breed/{breed}/images/random");
        return await _client.ExecuteGetAsync<SingleImageResponse>(request);
    }

    /// <summary>GET /breed/{breed}/images/random/{count} — múltiplas imagens de uma raça</summary>
    public async Task<RestResponse<MultipleImagesResponse>> GetMultipleRandomBreedImagesAsync(string breed, int count)
    {
        var request = new RestRequest($"breed/{breed}/images/random/{count}");
        return await _client.ExecuteGetAsync<MultipleImagesResponse>(request);
    }

    // ──────────────────────────────────────────────────────────────
    // Sub-breeds
    // ──────────────────────────────────────────────────────────────

    /// <summary>GET /breed/{breed}/list — lista sub-raças de uma raça</summary>
    public async Task<RestResponse<StringListResponse>> GetSubBreedsAsync(string breed)
    {
        var request = new RestRequest($"breed/{breed}/list");
        return await _client.ExecuteGetAsync<StringListResponse>(request);
    }

    /// <summary>GET /breed/{breed}/{subBreed}/images — todas as imagens de uma sub-raça</summary>
    public async Task<RestResponse<MultipleImagesResponse>> GetSubBreedImagesAsync(string breed, string subBreed)
    {
        var request = new RestRequest($"breed/{breed}/{subBreed}/images");
        return await _client.ExecuteGetAsync<MultipleImagesResponse>(request);
    }

    /// <summary>GET /breed/{breed}/{subBreed}/images/random — imagem aleatória de uma sub-raça</summary>
    public async Task<RestResponse<SingleImageResponse>> GetRandomSubBreedImageAsync(string breed, string subBreed)
    {
        var request = new RestRequest($"breed/{breed}/{subBreed}/images/random");
        return await _client.ExecuteGetAsync<SingleImageResponse>(request);
    }

    /// <summary>GET /breed/{breed}/{subBreed}/images/random/{count} — múltiplas imagens de uma sub-raça</summary>
    public async Task<RestResponse<MultipleImagesResponse>> GetMultipleRandomSubBreedImagesAsync(string breed, string subBreed, int count)
    {
        var request = new RestRequest($"breed/{breed}/{subBreed}/images/random/{count}");
        return await _client.ExecuteGetAsync<MultipleImagesResponse>(request);
    }

    public void Dispose() => _client.Dispose();
}
