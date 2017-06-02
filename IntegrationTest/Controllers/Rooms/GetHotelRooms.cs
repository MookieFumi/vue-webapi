using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTest.Infrastructure;
using VueWebApi;
using VueWebApi.ViewModels;
using Xunit;

namespace IntegrationTest.Controllers.Rooms
{
    public class GetHotelRooms : TestBase
    {
        public GetHotelRooms(TestFixture<Startup> fixture) : base(fixture) { }

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
