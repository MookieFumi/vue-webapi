using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using VueWebApi.ViewModels;
using Xunit;

namespace IntegrationTest.Rooms
{
    public class GetHotelRoomById : IClassFixture<TestFixture<VueWebApi.Startup>>
    {
        private TestServer Server { get; }

        public GetHotelRoomById(TestFixture<VueWebApi.Startup> fixture)
        {
            Server = fixture.Server;
        }

        [Fact]
        public async Task Can_Get_Hotel_Room_By_Id()
        {
            // Act
            const string url = "api/hotels/1/rooms/1";
            var response = await Server.CreateRequest(url)
                .GetAsync();

            var value = await response.Content.ReadAsAsync<RoomViewModel>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            value.Id.Should().Be(1);
        }

        [Fact]
        public async Task Can_Not_Get_Hotel_Room_By_Id_With_Not_Exist_Id()
        {
            // Act
            var url = $"api/hotels/1/rooms/{int.MaxValue}";
            var response = await Server.CreateRequest(url)
                .GetAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
