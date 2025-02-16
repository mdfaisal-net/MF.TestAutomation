using MF.Core.API.Framework.Extensions;
using MF.Core.API.Framework.Interfaces;
using MF.Core.API.Framework.Models;
using MF.Core.API.Framework.Utilites;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Core.API.Framework.Clients
{
    public class RestSharpClient : IApiClient
    {
        public async Task<Response> SendAsyc(Request request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            using (var httpClient = new RestClient(request.Url))
            {
                var restRequest = new RestRequest(request.Endpoint);

                // Add headers
                if (request.Headers != null)
                    restRequest.AddHeaders(request.Headers);

                // Add query parameters
                if (request.QueryParameters != null)
                    request.QueryParameters?.ToList().ForEach(p => restRequest.AddQueryParameter(p.Key, p.Value));

                // Add body
                if (request.Body != null)
                {
                    var serializedBody = SerializationHelper.Serialize(request.Body);
                    restRequest.AddJsonBody(serializedBody);
                }
                // Execute request
                var response = await httpClient.ExecuteAsync(restRequest, request.GetResharpMethod());
                var headers = response.Headers?.GroupBy(x => x.Name).ToDictionary(y => y.Key, y => string.Join("; ", y.Select(z => z.Value.ToString())));
                return new Response(response.Content, (int)response.StatusCode);
            }
        }
    }
}
