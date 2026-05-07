using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace DogApiTests.Tests;

[TestFixture]
[Category("SubBreed")]
public class SubBreedTests : BaseTest
{
    private const string Breed = "hound";
    private const string SubBreed = "afghan";

    // ──────────────────────────────────────────────────────────────
    // List sub-breeds
    // ──────────────────────────────────────────────────────────────

    [Test]
    [Description("GET /breed/hound/list deve retornar HTTP 200")]
    public async Task GetSubBreeds_ShouldReturn200()
    {
        var response = await Client.GetSubBreedsAsync(Breed);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    [Description("GET /breed/hound/list deve retornar status 'success'")]
    public async Task GetSubBreeds_StatusShouldBeSuccess()
    {
        var response = await Client.GetSubBreedsAsync(Breed);

        response.Data.Should().NotBeNull();
        response.Data!.Status.Should().Be("success");
    }

    [Test]
    [Description("GET /breed/hound/list deve retornar lista não vazia")]
    public async Task GetSubBreeds_ShouldReturnNonEmptyList()
    {
        var response = await Client.GetSubBreedsAsync(Breed);

        response.Data!.Message.Should().NotBeEmpty();
    }

    [Test]
    [Description("GET /breed/hound/list deve incluir 'afghan' como sub-raça")]
    public async Task GetSubBreeds_ShouldContainAfghan()
    {
        var response = await Client.GetSubBreedsAsync(Breed);

        response.Data!.Message.Should().Contain(SubBreed,
            "'afghan' é uma sub-raça conhecida de 'hound'");
    }

    [Test]
    [Description("GET /breed/poodle/list deve retornar lista não vazia (poodle tem sub-raças)")]
    public async Task GetSubBreeds_Poodle_ShouldReturnNonEmptyList()
    {
        var response = await Client.GetSubBreedsAsync("poodle");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Data!.Message.Should().NotBeEmpty();
    }

    // ──────────────────────────────────────────────────────────────
    // All images from a sub-breed
    // ──────────────────────────────────────────────────────────────

    [Test]
    [Description("GET /breed/hound/afghan/images deve retornar HTTP 200")]
    public async Task GetSubBreedImages_ShouldReturn200()
    {
        var response = await Client.GetSubBreedImagesAsync(Breed, SubBreed);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    [Description("GET /breed/hound/afghan/images deve retornar status 'success'")]
    public async Task GetSubBreedImages_StatusShouldBeSuccess()
    {
        var response = await Client.GetSubBreedImagesAsync(Breed, SubBreed);

        response.Data!.Status.Should().Be("success");
    }

    [Test]
    [Description("GET /breed/hound/afghan/images deve retornar lista não vazia")]
    public async Task GetSubBreedImages_ShouldReturnNonEmptyList()
    {
        var response = await Client.GetSubBreedImagesAsync(Breed, SubBreed);

        response.Data!.Message.Should().NotBeEmpty();
    }

    [Test]
    [Description("GET /breed/hound/afghan/images — URLs devem conter 'hound-afghan' ou sub-breed path")]
    public async Task GetSubBreedImages_UrlsShouldContainSubBreedPath()
    {
        var response = await Client.GetSubBreedImagesAsync(Breed, SubBreed);

        response.Data!.Message.Should().AllSatisfy(url =>
            url.Should().Contain(SubBreed,
                "as imagens devem pertencer à sub-raça solicitada"));
    }

    // ──────────────────────────────────────────────────────────────
    // Random image from sub-breed
    // ──────────────────────────────────────────────────────────────

    [Test]
    [Description("GET /breed/hound/afghan/images/random deve retornar HTTP 200")]
    public async Task GetRandomSubBreedImage_ShouldReturn200()
    {
        var response = await Client.GetRandomSubBreedImageAsync(Breed, SubBreed);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    [Description("GET /breed/hound/afghan/images/random deve retornar URL de imagem válida")]
    public async Task GetRandomSubBreedImage_ShouldReturnValidUrl()
    {
        var response = await Client.GetRandomSubBreedImageAsync(Breed, SubBreed);

        response.Data.Should().NotBeNull();
        AssertIsImageUrl(response.Data!.Message);
    }

    [Test]
    [Description("GET /breed/hound/afghan/images/random deve retornar status 'success'")]
    public async Task GetRandomSubBreedImage_StatusShouldBeSuccess()
    {
        var response = await Client.GetRandomSubBreedImageAsync(Breed, SubBreed);

        response.Data!.Status.Should().Be("success");
    }

    // ──────────────────────────────────────────────────────────────
    // Multiple random images from sub-breed
    // ──────────────────────────────────────────────────────────────

    [Test]
    [Description("GET /breed/hound/afghan/images/random/3 deve retornar exatamente 3 imagens")]
    public async Task GetMultipleRandomSubBreedImages_ShouldReturnRequestedCount()
    {
        var response = await Client.GetMultipleRandomSubBreedImagesAsync(Breed, SubBreed, 3);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Data!.Message.Should().HaveCount(3);
    }

    [Test]
    [Description("GET /breed/hound/afghan/images/random/3 — URLs devem ser únicas")]
    public async Task GetMultipleRandomSubBreedImages_UrlsShouldBeUnique()
    {
        var response = await Client.GetMultipleRandomSubBreedImagesAsync(Breed, SubBreed, 3);

        response.Data!.Message.Should().OnlyHaveUniqueItems();
    }

    [Test]
    [Description("GET /breed/hound/afghan/images/random/3 deve retornar status 'success'")]
    public async Task GetMultipleRandomSubBreedImages_StatusShouldBeSuccess()
    {
        var response = await Client.GetMultipleRandomSubBreedImagesAsync(Breed, SubBreed, 3);

        response.Data!.Status.Should().Be("success");
    }

    [Test]
    [Description("GET /breed/hound/afghan/images/random/3 — todas as URLs devem ser imagens válidas")]
    public async Task GetMultipleRandomSubBreedImages_AllUrlsShouldBeValid()
    {
        var response = await Client.GetMultipleRandomSubBreedImagesAsync(Breed, SubBreed, 3);

        response.Data!.Message.Should().AllSatisfy(url => AssertIsImageUrl(url));
    }
}
