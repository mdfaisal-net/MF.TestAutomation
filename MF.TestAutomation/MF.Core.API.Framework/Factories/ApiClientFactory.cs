using System.Net.Http;
using Microsoft.Playwright;
using RestSharp;
using System.Threading.Tasks;
using MF.Core.API.Framework.Interfaces;
using MF.Core.API.Framework.Enums;
using MF.Core.API.Framework.Clients;
using MF.Core.API.Framework.Models;

namespace MF.Core.API.Framework.Factories
{
    public class ApiClientFactory
    {
        public static IApiClient CreateClient(ApiClientType clientType, IHttpClientFactory httpClientFactory)
        {
            return clientType switch
            {
                ApiClientType.HttpClient => new HTTPClient(httpClientFactory.CreateClient()),
                ApiClientType.RestSharp => new RestSharpClient(),
                ApiClientType.Playwright => new PlaywrightClient(),
                _ => throw new ArgumentException("Invalid client type", nameof(clientType))
            };
        }
    }
}
