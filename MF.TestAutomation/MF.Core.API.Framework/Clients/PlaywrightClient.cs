using MF.Core.API.Framework.Enums;
using MF.Core.API.Framework.Extensions;
using MF.Core.API.Framework.Interfaces;
using MF.Core.API.Framework.Models;
using MF.Core.API.Framework.Utilites;
using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Core.API.Framework.Clients
{
    class PlaywrightClient : IApiClient
    {
        public async Task<Response> SendAsyc(Request request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();

            var url = request.Url;
            if (request.Endpoint != null)
                url = $"{url}/{request.Endpoint}";

            if (request.QueryParameters != null && request.QueryParameters.Count() > 0)
                url = $"{url}?{request.ToQueryString()}";

            var playwrightRequest = await playwright.APIRequest.NewContextAsync(new()
            {
                BaseURL = url,
                ExtraHTTPHeaders = request.Headers,
            });

            IAPIResponse response = request.Method switch
            {
                HttpMethodType.GET => await playwrightRequest.GetAsync(url),
                HttpMethodType.POST => await playwrightRequest.PostAsync(url, new()
                {
                    Headers = request.Headers,
                    DataObject = request.Body
                }),
                HttpMethodType.PUT => await playwrightRequest.PutAsync(url, new()
                {
                    Headers = request.Headers,
                    DataObject = request.Body
                }),
                HttpMethodType.DELETE => await playwrightRequest.DeleteAsync(url, new()
                {
                    Headers = request.Headers
                }),
                HttpMethodType.PATCH => await playwrightRequest.PatchAsync(url, new()
                {
                    Headers = request.Headers,
                    DataObject = request.Body
                }),
                _ => throw new ArgumentException("Invalid HTTP method", nameof(request.Method))
            };

            var content = await response.TextAsync();
            var statusCode = response.Status;

            await browser.CloseAsync();

            return new Response(content, statusCode);
        }
    }
}
