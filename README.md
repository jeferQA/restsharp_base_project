# DogApiTests — RestSharp + NUnit

Projeto de testes automatizados para a [Dog CEO API](https://dog.ceo/dog-api/documentation/), construído com **RestSharp** e **NUnit**.

---

## Estrutura do Projeto

```
DogApiTests/
├── Models/
│   └── DogApiModels.cs          # DTOs de deserialização das respostas
├── Services/
│   └── DogApiClient.cs          # Cliente HTTP (RestSharp) com todos os endpoints
└── Tests/
    ├── BaseTest.cs              # Classe base com SetUp/TearDown e helpers
    ├── BreedsTests.cs           # Testes para GET /breeds/list/all
    ├── RandomImageTests.cs      # Testes para /breeds/image/random e /random/{count}
    ├── ByBreedTests.cs          # Testes para /breed/{breed}/images e variantes
    └── SubBreedTests.cs         # Testes para /breed/{breed}/{subBreed}/... e variantes
```

---

## Dependências

| Pacote                   | Versão  | Função                          |
|--------------------------|---------|---------------------------------|
| RestSharp                | 112.x   | Cliente HTTP                    |
| NUnit                    | 4.x     | Framework de testes             |
| NUnit3TestAdapter        | 4.x     | Adapter para `dotnet test`      |
| FluentAssertions         | 6.x     | Assertions expressivas          |
| Newtonsoft.Json          | 13.x    | Serialização/Deserialização JSON|
| Microsoft.NET.Test.Sdk   | 17.x    | SDK de teste .NET               |

---

## Como Executar

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Restaurar e rodar todos os testes
```bash
dotnet restore
dotnet test
```

### Filtrar por categoria
```bash
# Apenas testes de raças
dotnet test --filter "Category=Breeds"

# Apenas testes de imagem aleatória
dotnet test --filter "Category=RandomImage"

# Apenas testes por raça
dotnet test --filter "Category=ByBreed"

# Apenas testes de sub-raça
dotnet test --filter "Category=SubBreed"
```

### Gerar relatório detalhado
```bash
dotnet test --logger "console;verbosity=detailed"
```

---

## Cobertura de Endpoints

| Endpoint                                       | Método | Testes |
|------------------------------------------------|--------|--------|
| `/breeds/list/all`                             | GET    | ✅ 6   |
| `/breeds/image/random`                         | GET    | ✅ 4   |
| `/breeds/image/random/{count}`                 | GET    | ✅ 6   |
| `/breed/{breed}/images`                        | GET    | ✅ 5   |
| `/breed/{breed}/images/random`                 | GET    | ✅ 5   |
| `/breed/{breed}/images/random/{count}`         | GET    | ✅ 4   |
| `/breed/{breed}/list`                          | GET    | ✅ 5   |
| `/breed/{breed}/{subBreed}/images`             | GET    | ✅ 4   |
| `/breed/{breed}/{subBreed}/images/random`      | GET    | ✅ 3   |
| `/breed/{breed}/{subBreed}/images/random/{n}`  | GET    | ✅ 4   |

**Total: ~46 testes** cobrindo status HTTP, estrutura da resposta, validação de URLs e comportamento de erros.

---

## 🏗️ Arquitetura

- **`DogApiClient`** centraliza todas as chamadas HTTP com RestSharp, retornando `RestResponse<T>` tipado.
- **`BaseTest`** gerencia o ciclo de vida do client (`SetUp`/`TearDown`) e expõe o helper `AssertIsImageUrl`.
- **FluentAssertions** torna as asserções legíveis e com mensagens de erro claras.
- Testes parametrizados com `[TestCase]` cobrem múltiplas raças em uma única definição de teste.
