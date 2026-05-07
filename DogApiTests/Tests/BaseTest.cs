using DogApiTests.Services;
using NUnit.Framework;

namespace DogApiTests.Tests;

/// <summary>
/// Classe base para todos os testes — gerencia o ciclo de vida do DogApiClient.
/// </summary>
public abstract class BaseTest
{
    protected DogApiClient Client = null!;

    [SetUp]
    public void SetUp()
    {
        Client = new DogApiClient();
    }

    [TearDown]
    public void TearDown()
    {
        Client?.Dispose();
    }

    /// <summary>
    /// Valida que uma URL retornada pela API é uma URL de imagem válida.
    /// </summary>
    protected static void AssertIsImageUrl(string url)
    {
        Assert.That(url, Is.Not.Null.And.Not.Empty, "URL da imagem não deve ser nula ou vazia");
        Assert.That(Uri.TryCreate(url, UriKind.Absolute, out _), Is.True,
            $"'{url}' não é uma URL válida");
        Assert.That(url.StartsWith("https://images.dog.ceo/"), Is.True,
            $"URL inesperada: {url}");
    }
}
