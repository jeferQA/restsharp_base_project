using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace DogApiTests.Tests;

[TestFixture]
[Category("RandomImage")]
public class RandomImageTests : BaseTest
{
    // ──────────────────────────────────────────────────────────────
    // Single random image
    // ──────────────────────────────────────────────────────────────

    [Test]
    [Description("GET /breeds/image/random deve retornar HTTP 200")]
    public async Task GetRandomImage_ShouldReturn200()
    {
        var response = await Client.GetRandomImageAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    [Description("GET /breeds/image/random deve retornar status 'success'")]
    public async Task GetRandomImage_StatusShouldBeSuccess()
    {
        var response = await Client.GetRandomImageAsync();

        response.Data.Should().NotBeNull();
        response.Data!.Status.Should().Be("success");
    }

    [Test]
    [Description("GET /breeds/image/random deve retornar uma URL de imagem válida")]
    public async Task GetRandomImage_MessageShouldBeValidImageUrl()
    {
        var response = await Client.GetRandomImageAsync();

        response.Data.Should().NotBeNull();
        AssertIsImageUrl(response.Data!.Message);
    }

    [Test]
    [Description("GET /breeds/image/random — duas chamadas consecutivas devem retornar URLs distintas (alta probabilidade)")]
    public async Task GetRandomImage_TwoCallsShouldReturnDifferentUrls()
    {
        var response1 = await Client.GetRandomImageAsync();
        var response2 = await Client.GetRandomImageAsync();

        // Probabilidade de colisão é praticamente nula com >20k imagens
        response1.Data!.Message.Should().NotBe(response2.Data!.Message,
            "imagens aleatórias raramente devem ser iguais");
    }

    // ──────────────────────────────────────────────────────────────
    // Multiple random images
    // ──────────────────────────────────────────────────────────────

    [Test]
    [Description("GET /breeds/image/random/3 deve retornar exatamente 3 imagens")]
    public async Task GetMultipleRandomImages_ShouldReturnRequestedCount()
    {
        var response = await Client.GetMultipleRandomImagesAsync(3);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Data.Should().NotBeNull();
        response.Data!.Message.Should().HaveCount(3);
    }

    [Test]
    [Description("GET /breeds/image/random/1 deve retornar exatamente 1 imagem")]
    public async Task GetMultipleRandomImages_WithCount1_ShouldReturn1Image()
    {
        var response = await Client.GetMultipleRandomImagesAsync(1);

        response.Data!.Message.Should().HaveCount(1);
    }

    [Test]
    [Description("GET /breeds/image/random/50 deve retornar exatamente 50 imagens (máximo documentado)")]
    public async Task GetMultipleRandomImages_WithMaxCount_ShouldReturn50Images()
    {
        var response = await Client.GetMultipleRandomImagesAsync(50);

        response.Data!.Message.Should().HaveCount(50);
    }

    [Test]
    [Description("GET /breeds/image/random/5 — todas as URLs devem ser imagens válidas")]
    public async Task GetMultipleRandomImages_AllUrlsShouldBeValid()
    {
        var response = await Client.GetMultipleRandomImagesAsync(5);

        response.Data.Should().NotBeNull();
        response.Data!.Message.Should().AllSatisfy(url => AssertIsImageUrl(url));
    }

    [Test]
    [Description("GET /breeds/image/random/5 — as URLs devem ser únicas")]
    public async Task GetMultipleRandomImages_UrlsShouldBeUnique()
    {
        var response = await Client.GetMultipleRandomImagesAsync(5);

        response.Data!.Message.Should().OnlyHaveUniqueItems("imagens retornadas não devem se repetir");
    }

    [Test]
    [Description("GET /breeds/image/random/5 deve retornar status 'success'")]
    public async Task GetMultipleRandomImages_StatusShouldBeSuccess()
    {
        var response = await Client.GetMultipleRandomImagesAsync(5);

        response.Data!.Status.Should().Be("success");
    }
}
