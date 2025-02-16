using MF.Core.API.Framework.Clients;
using MF.Core.API.Framework.Enums;
using MF.Core.API.Framework.Factories;
using MF.Core.API.Framework.Interfaces;
using MF.TestAutomation.Test.Interfaces;
using MF.TestAutomation.Test.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.TestAutomation.Test
{
    public class ConfigureServices
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<ITokenGenerator, TokenGenerator>();

            services.AddHttpClient();

            services.AddSingleton<IApiClient>(services =>
            {
                var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
                return ApiClientFactory.CreateClient(ApiClientType.RestSharp, httpClientFactory);
            });
        }
    }
}
