using MF.Core.API.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Core.API.Framework.Models
{
    public class RequestBuilder
    {
        private readonly Request _request;
        public RequestBuilder()
        {
            _request = new Request
            {
                Url = string.Empty,
                Headers = new Dictionary<string, string>(),
                QueryParameters = new Dictionary<string, string>()
            };
        }
        public RequestBuilder WithUrl(string url)
        {
            _request.Url = url;
            return this;
        }
        public RequestBuilder WithEndpoint(string endpoint)
        {
            _request.Endpoint = endpoint;
            return this;
        }

        public RequestBuilder WithMethod(HttpMethodType method)
        {
            _request.Method = method;
            return this;
        }
        public RequestBuilder WithContentType(string contentType)
        {
            _request.ContentType = contentType;
            return this;
        }
        public RequestBuilder WithHeaders(IDictionary<string, string> headers)
        {
            _request.Headers = headers;
            return this;
        }

        public RequestBuilder WithHeaders(string key, string value)
        {
            _request.Headers[key] = value;
            return this;
        }

        public RequestBuilder WithQueryParameters(IDictionary<string, string> queryParameters)
        {
            _request.QueryParameters = queryParameters;
            return this;
        }
        public RequestBuilder WithQueryParameters(string key, string value)
        {
            _request.QueryParameters[key] = value;
            return this;
        }

        public RequestBuilder WithBody(object body)
        {
            _request.Body = body;
            return this;
        }

        public RequestBuilder WithJsonBody(object body)
        {
            _request.Body = body;
            WithContentType("application/json");
            return this;
        }

        public RequestBuilder WithXmlBody(object body)
        {
            _request.Body = body;
            WithContentType("application/xml");
            return this;
        }

        public RequestBuilder WithFormUrlEncodedBody(object body)
        {
            _request.Body = body;
            WithContentType("application/x-www-form-urlencoded");
            return this;
        }

        public RequestBuilder WithTextBody(object body)
        {
            _request.Body = body;
            WithContentType("text/plain");
            return this;
        }

        public RequestBuilder WithHtmlBody(object body)
        {
            _request.Body = body;
            WithContentType("text/html");
            return this;
        }


        public Request Build()
        {
            return _request;
        }
        public RequestBuilder WithAuthorizationToken(string token)
        {
            _request.Headers["Authorization"] = $"Bearer {token}";
            return this;
        }

        public RequestBuilder Get()
        {
            _request.Method = HttpMethodType.GET;
            return this;
        }

        public RequestBuilder Post()
        {
            _request.Method = HttpMethodType.POST;
            return this;
        }

        public RequestBuilder Put()
        {
            _request.Method = HttpMethodType.PUT;
            return this;
        }

        public RequestBuilder Delete()
        {
            _request.Method = HttpMethodType.DELETE;
            return this;
        }

        public RequestBuilder Patch()
        {
            _request.Method = HttpMethodType.PATCH;
            return this;
        }
    }
}
