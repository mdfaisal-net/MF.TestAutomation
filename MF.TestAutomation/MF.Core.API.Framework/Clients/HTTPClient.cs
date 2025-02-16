using MF.Core.API.Framework.Extensions;
using MF.Core.API.Framework.Interfaces;
using MF.Core.API.Framework.Models;
using MF.Core.API.Framework.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MF.Core.API.Framework.Clients
{
    public class HTTPClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        public HTTPClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response> SendAsyc(Request request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            
            var httpRequest = new HttpRequestMessage
            {
                Method = request.GetHttpMethod()
            };

            if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var uri))
                throw new UriFormatException($"Invalid baes URL: {request.Url}");

            _httpClient.BaseAddress = uri;

            if (!Uri.TryCreate(request.Endpoint ?? "", UriKind.RelativeOrAbsolute, out var requestUri))
                throw new UriFormatException($"Invalid request URL: {request.Endpoint}");

            if (!requestUri.IsAbsoluteUri)
                httpRequest.RequestUri = new Uri(_httpClient.BaseAddress, requestUri);
            else
                httpRequest.RequestUri = requestUri;

            if (request.Headers != null)
            {
                foreach (var key in request.Headers.Keys)
                {
                    if (_httpClient.DefaultRequestHeaders.TryGetValues(key, out var value))
                        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, value);
                }
            }

            if (request.QueryParameters != null && request.QueryParameters.Count() > 0)
                httpRequest.RequestUri = new Uri($"{httpRequest.RequestUri}?{request.ToQueryString()}");

            if(request.Body != null)
            {
                var json = SerializationHelper.Serialize(request.Body);
                httpRequest.Content = new StringContent(json, Encoding.UTF8, request.ContentType ?? "application/json");
            }

            var response = await _httpClient.SendAsync(httpRequest);
            var content = await response.Content.ReadAsStringAsync();
            var headers = response.Headers?.ToDictionary(x => x.Key, x => string.Join("; ", x.Value));

            return new Response(content, (int)response.StatusCode);
        }
    }
}
