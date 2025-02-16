# MF.TestAutomation
# Core API Framework

This project is a core API framework designed to support multiple HTTP clients, including `HttpClient`, `RestSharp`, and `Playwright`. It provides a flexible and extensible way to create and send API requests using different clients based on the requirements.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Features

- Support for multiple HTTP clients: `HttpClient`, `RestSharp`, and `Playwright`.
- Flexible request building with support for headers, query parameters, and request body.
- Easy integration with dependency injection.
- Extensible design to add more clients if needed.

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or later

### Installation

1. Clone the repository:
    
2. Navigate to the project directory:
    
3. Open the solution in Visual Studio:
    
4. Restore the NuGet packages:
    
## Usage

### Configuring Services

In your test project, configure the services in `ConfigureServices.cs`:

using MF.Core.API.Framework.Clients; using MF.Core.API.Framework.Enums; using MF.Core.API.Framework.Factories; using MF.Core.API.Framework.Interfaces; using MF.TestAutomation.Test.Interfaces; using MF.TestAutomation.Test.Utilities; using Microsoft.Extensions.DependencyInjection;
namespace MF.TestAutomation.Test { public class ConfigureServices { public static void Configure(IServiceCollection services) { services.AddSingleton<ITokenGenerator, TokenGenerator>(); services.AddHttpClient();
        services.AddSingleton<IApiClient>(services =>
        {
            var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
            return ApiClientFactory.CreateClient(ApiClientType.RestSharp, httpClientFactory);
        });
    }
}
}


### Creating and Sending Requests

Use the `RequestBuilder` to create requests and send them using the configured `IApiClient`:

using MF.Core.API.Framework.Models; using MF.TestAutomation.Test.Utilities; using System.Net;
namespace MF.TestAutomation.Test.Tests { public class Tests : BaseTest { private IDictionary<string, string> _headers; private string BaseURL;
    [SetUp]
    public void Setup()
    {
        _headers = ApiHelper.GetDefaultHeaders();
        BaseURL = "https://api.example.com";
    }

    [Test]
    public async Task POST_Books_Should_Return_201()
    {
        // Arrange
        var token = TokenGenerator?.GenerateTokenAsync().Result;
        if (token == null)
        {
            Assert.Fail("Token generation failed.");
        }
        _headers.Add("Authorization", $"Bearer {token}");

        var request = new RequestBuilder()
            .WithUrl(BaseURL)
            .WithEndpoint("/endpoint")
            .WithHeaders(_headers)
            .WithQueryParameters("key", "value")
            .WithJsonBody(new { key = "value" })
            .Build();

        // Act
        var response = await ApiClient.SendAsyc(request);

        // Assert
        Assert.IsNotNull(response.Content);
        Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
    }
}
}


## Project Structure

MF.Core.API.Framework/
├── Clients/
│   ├── HTTPClient.cs
│   ├── PlaywrightClient.cs
│   └── RestSharpClient.cs
├── Enums/
│   └── HttpMethodType.cs
├── Factories/
│   └── ApiClientFactory.cs
├── Interfaces/
│   └── IApiClient.cs
├── Models/
│   └── Request.cs
├── Utilities/
│   └── SerializationHelper.cs
├── MF.Core.API.Framework.csproj

MF.TestAutomation.Test/
├── Tests/
│   ├── BaseTest.cs
│   └── UnitTest1.cs
├── Utilities/
│   └── ApiHelper.cs
├── ConfigureServices.cs
├── MF.TestAutomation.Test.csproj


## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements or bug fixes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
