using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Threading.Tasks;
using VueWebApi.ViewModels;
using Xunit;

namespace IntegrationTest.Hotels
{
    public class GetHotels : IClassFixture<TestFixture<VueWebApi.Startup>>
    {
        private TestServer Server { get; }

        public GetHotels(TestFixture<VueWebApi.Startup> fixture)
        {
            Server = fixture.Server;
        }

        [Fact]
        public async Task Can_Get_Hotels()
        {
            // Action
            const string url = "api/hotels";
            var response = await Server.CreateRequest(url)
               .GetAsync();

            var values = await response.Content.ReadAsAsync<HotelViewModel[]>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            values.Length.Should().BeGreaterThan(0);
        }
    }
}
