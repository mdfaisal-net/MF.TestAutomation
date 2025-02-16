using MF.Core.API.Framework.Models;
using MF.TestAutomation.Test.Utilities;
using System.Net;

namespace MF.TestAutomation.Test.Tests
{
    public class Tests : BaseTest
    {
        private IDictionary<string, string> _headers;
        private string BaseURL;
        [SetUp]
        public void Setup()
        {
            _headers = ApiHelper.GetDefaultHeaders();
            BaseURL = "https://api.example.com";
        }

        [Test]
        public async Task POST_Books_Should_Return_201()
        {
            //Arrange
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

            //Act
            var response = await ApiClient.SendAsyc(request);

            //Assert
            Assert.IsNotNull(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
        }
    }
}