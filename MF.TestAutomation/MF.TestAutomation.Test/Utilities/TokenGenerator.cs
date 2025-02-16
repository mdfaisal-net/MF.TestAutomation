using MF.Core.API.Framework.Utilites;
using MF.TestAutomation.Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MF.TestAutomation.Test.Utilities
{
    public class TokenGenerator : ITokenGenerator
    {
        private const string AuthenticatedURL = "https://www.dummyapi.com/auth/login";
        private const string Username = "your_username";
        private const string Password = "your_password";

        public async Task<string> GenerateTokenAsync()
        {
            using var httpClient = new HttpClient();
            var credentials = new { username = Username, password = Password };
            var content = new StringContent(SerializationHelper.Serialize(credentials), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(AuthenticatedURL, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(responseBody);
            var accessToken = jsonDocument.RootElement.GetProperty("access_token").GetString();

            return accessToken;
        }
    }
}
