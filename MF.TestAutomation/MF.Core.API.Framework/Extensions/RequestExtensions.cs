using MF.Core.API.Framework.Enums;
using MF.Core.API.Framework.Models;
using Microsoft.Playwright;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Core.API.Framework.Extensions
{
    public static class RequestExtensions
    {
        public static string ToQueryString(this Request request)
            => string.Join("&", request.QueryParameters.Select(x => $"{x.Key}={x.Value}"));

        public static HttpMethod GetHttpMethod(this Request request)
        {
            return request.Method switch
            {
                HttpMethodType.GET => HttpMethod.Get,
                HttpMethodType.POST => HttpMethod.Post,
                HttpMethodType.PUT => HttpMethod.Put,
                HttpMethodType.DELETE => HttpMethod.Delete,
                HttpMethodType.PATCH => HttpMethod.Patch,
                _ => HttpMethod.Get
            };
        }

        public static Method GetResharpMethod(this Request request)
        {
            return request.Method switch
            {
                HttpMethodType.GET => Method.Get,
                HttpMethodType.POST => Method.Post,
                HttpMethodType.PUT => Method.Put,
                HttpMethodType.DELETE => Method.Delete,
                HttpMethodType.PATCH => Method.Patch,
                _ => Method.Get
            };
        }
    }
}
