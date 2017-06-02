using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using VueWebApi.ViewModels;
using Xunit;

namespace IntegrationTest.Rooms
{
    public class GetHotelRooms : IClassFixture<TestFixture<VueWebApi.Startup>>
    {
        private TestServer Server { get; }

        public GetHotelRooms(TestFixture<VueWebApi.Startup> fixture)
        {
            Server = fixture.Server;
        }

        [Fact]
        public async Task Can_Get_Hotel_Rooms()
        {
            // Action
            const string url = "api/hotels/1/rooms";
            var response = await Server.CreateRequest(url)
                .GetAsync();

            var values = await response.Content.ReadAsAsync<RoomViewModel[]>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            values.Length.Should().BeGreaterThan(0);
        }
    }
}
