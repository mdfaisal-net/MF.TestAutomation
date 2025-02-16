using MF.Core.API.Framework.Clients;
using MF.Core.API.Framework.Interfaces;
using MF.TestAutomation.Test.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.TestAutomation.Test.Tests
{
    public class BaseTest
    {
        protected IApiClient? ApiClient { get; private set; }

        protected ITokenGenerator? TokenGenerator { get; private set; }
        protected ServiceProvider? ServiceProvider { get; private set; }

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();
            services.AddHttpClient<IApiClient, RestSharpClient>();
            ServiceProvider = services.BuildServiceProvider();
            ApiClient = ServiceProvider.GetService<IApiClient>();
            TokenGenerator = ServiceProvider.GetService<ITokenGenerator>();
        }

        [TearDown]
        public void TearDown()
        {
            ServiceProvider?.Dispose();
        }
    }
}
