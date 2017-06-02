using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace IntegrationTest.Infrastructure
{
    public static class RequestBuilderExtensions
    {
        public static RequestBuilder WithContent(this RequestBuilder requestBuilder, object data)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            requestBuilder
                .And(x => x.Content = new StringContent(serializedData, Encoding.UTF8, "application/json"));

            return requestBuilder;
        }
    }
}
