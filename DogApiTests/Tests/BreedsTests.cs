using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace DogApiTests.Tests;

[TestFixture]
[Category("Breeds")]
public class BreedsTests : BaseTest
{
    [Test]
    [Description("GET /breeds/list/all deve retornar HTTP 200")]
    public async Task GetAllBreeds_ShouldReturn200()
    {
        var response = await Client.GetAllBreedsAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK,
            "a API deve responder com sucesso");
    }

    [Test]
    [Description("GET /breeds/list/all deve retornar status 'success'")]
    public async Task GetAllBreeds_StatusShouldBeSuccess()
    {
        var response = await Client.GetAllBreedsAsync();

        response.Data.Should().NotBeNull();
        response.Data!.Status.Should().Be("success");
    }

    [Test]
    [Description("GET /breeds/list/all deve retornar pelo menos 100 raças")]
    public async Task GetAllBreeds_ShouldReturnAtLeast100Breeds()
    {
        var response = await Client.GetAllBreedsAsync();

        response.Data.Should().NotBeNull();
        response.Data!.Message.Should().NotBeNullOrEmpty();
        response.Data.Message.Count.Should().BeGreaterThanOrEqualTo(100,
            "a API documenta mais de 120 raças");
    }

    [Test]
    [Description("GET /breeds/list/all — cada chave do dicionário deve ser uma string não vazia")]
    public async Task GetAllBreeds_AllKeysShouldBeNonEmpty()
    {
        var response = await Client.GetAllBreedsAsync();

        response.Data!.Message.Keys.Should().AllSatisfy(k =>
            k.Should().NotBeNullOrWhiteSpace("nomes de raça não devem ser vazios"));
    }

    [Test]
    [Description("GET /breeds/list/all — raça 'hound' deve existir na lista")]
    public async Task GetAllBreeds_ShouldContainHound()
    {
        var response = await Client.GetAllBreedsAsync();

        response.Data!.Message.Should().ContainKey("hound",
            "'hound' é uma das raças mais conhecidas na API");
    }

    [Test]
    [Description("GET /breeds/list/all — raça 'hound' deve ter sub-raças")]
    public async Task GetAllBreeds_HoundShouldHaveSubBreeds()
    {
        var response = await Client.GetAllBreedsAsync();

        response.Data!.Message["hound"].Should().NotBeEmpty(
            "'hound' possui sub-raças documentadas como 'afghan', 'basset', etc.");
    }
}
