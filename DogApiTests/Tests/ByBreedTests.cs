using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace DogApiTests.Tests;

[TestFixture]
[Category("ByBreed")]
public class ByBreedTests : BaseTest
{
    private const string ValidBreed = "hound";
    private const string InvalidBreed = "xyzinvalidbreed";

    // ──────────────────────────────────────────────────────────────
    // All images by breed
    // ──────────────────────────────────────────────────────────────

    [Test]
    [Description("GET /breed/hound/images deve retornar HTTP 200")]
    public async Task GetBreedImages_ShouldReturn200()
    {
        var response = await Client.GetBreedImagesAsync(ValidBreed);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    [Description("GET /breed/hound/images deve retornar status 'success'")]
    public async Task GetBreedImages_StatusShouldBeSuccess()
    {
        var response = await Client.GetBreedImagesAsync(ValidBreed);

        response.Data.Should().NotBeNull();
        response.Data!.Status.Should().Be("success");
    }

    [Test]
    [Description("GET /breed/hound/images deve retornar lista não vazia de imagens")]
    public async Task GetBreedImages_ShouldReturnNonEmptyList()
    {
        var response = await Client.GetBreedImagesAsync(ValidBreed);

        response.Data!.Message.Should().NotBeEmpty();
    }

    [Test]
    [Description("GET /breed/hound/images — todas as URLs devem conter 'hound' no path")]
    public async Task GetBreedImages_AllUrlsShouldContainBreedName()
    {
        var response = await Client.GetBreedImagesAsync(ValidBreed);

        response.Data!.Message.Should().AllSatisfy(url =>
            url.Should().Contain(ValidBreed,
                "as imagens retornadas devem pertencer à raça solicitada"));
    }

    [Test]
    [Description("GET /breed/invalid/images deve retornar HTTP 404")]
    public async Task GetBreedImages_WithInvalidBreed_ShouldReturn404()
    {
        var response = await Client.GetBreedImagesAsync(InvalidBreed);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // ──────────────────────────────────────────────────────────────
    // Random image by breed
    // ──────────────────────────────────────────────────────────────

    [Test]
    [Description("GET /breed/hound/images/random deve retornar HTTP 200")]
    public async Task GetRandomBreedImage_ShouldReturn200()
    {
        var response = await Client.GetRandomBreedImageAsync(ValidBreed);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    [Description("GET /breed/hound/images/random deve retornar URL de imagem válida")]
    public async Task GetRandomBreedImage_ShouldReturnValidUrl()
    {
        var response = await Client.GetRandomBreedImageAsync(ValidBreed);

        response.Data.Should().NotBeNull();
        AssertIsImageUrl(response.Data!.Message);
    }

    [Test]
    [Description("GET /breed/hound/images/random — URL deve conter 'hound'")]
    public async Task GetRandomBreedImage_UrlShouldContainBreedName()
    {
        var response = await Client.GetRandomBreedImageAsync(ValidBreed);

        response.Data!.Message.Should().Contain(ValidBreed);
    }

    [Test]
    [Description("GET /breed/hound/images/random deve retornar status 'success'")]
    public async Task GetRandomBreedImage_StatusShouldBeSuccess()
    {
        var response = await Client.GetRandomBreedImageAsync(ValidBreed);

        response.Data!.Status.Should().Be("success");
    }

    // ──────────────────────────────────────────────────────────────
    // Multiple random images by breed
    // ──────────────────────────────────────────────────────────────

    [Test]
    [Description("GET /breed/hound/images/random/3 deve retornar exatamente 3 imagens")]
    public async Task GetMultipleRandomBreedImages_ShouldReturnRequestedCount()
    {
        var response = await Client.GetMultipleRandomBreedImagesAsync(ValidBreed, 3);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Data!.Message.Should().HaveCount(3);
    }

    [Test]
    [Description("GET /breed/hound/images/random/3 — URLs devem ser únicas")]
    public async Task GetMultipleRandomBreedImages_UrlsShouldBeUnique()
    {
        var response = await Client.GetMultipleRandomBreedImagesAsync(ValidBreed, 3);

        response.Data!.Message.Should().OnlyHaveUniqueItems();
    }

    [Test]
    [Description("GET /breed/hound/images/random/3 — todas as URLs devem conter 'hound'")]
    public async Task GetMultipleRandomBreedImages_AllUrlsShouldContainBreedName()
    {
        var response = await Client.GetMultipleRandomBreedImagesAsync(ValidBreed, 3);

        response.Data!.Message.Should().AllSatisfy(url =>
            url.Should().Contain(ValidBreed));
    }

    [Test]
    [Description("GET /breed/hound/images/random/3 deve retornar status 'success'")]
    public async Task GetMultipleRandomBreedImages_StatusShouldBeSuccess()
    {
        var response = await Client.GetMultipleRandomBreedImagesAsync(ValidBreed, 3);

        response.Data!.Status.Should().Be("success");
    }

    // ──────────────────────────────────────────────────────────────
    // Parametrized test com múltiplas raças
    // ──────────────────────────────────────────────────────────────

    [Test]
    [TestCase("labrador")]
    [TestCase("poodle")]
    [TestCase("retriever")]
    [TestCase("bulldog")]
    [Description("GET /breed/{breed}/images/random deve funcionar para múltiplas raças")]
    public async Task GetRandomBreedImage_ShouldWorkForMultipleBreeds(string breed)
    {
        var response = await Client.GetRandomBreedImageAsync(breed);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Data!.Status.Should().Be("success");
        response.Data.Message.Should().NotBeNullOrEmpty();
    }
}
