using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTest.Infrastructure;
using VueWebApi;
using VueWebApi.ViewModels;
using Xunit;

namespace IntegrationTest.Controllers.Rooms
{
    public class CreateHotelRoom : TestBase
    {
        public CreateHotelRoom(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task Can_Create_Hotel_Room()
        {
            // Arrange
            var model = new RoomViewModel()
            {
                Name = Utils.RandomString(256),
                Price = 30,
                Vat = 21
            };

            // Act
            const string url = "api/hotels/1/rooms";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Can_Not_Create_Hotel_Room_Without_Name()
        {
            // Arrange
            var model = new RoomViewModel
            {
                Price = 30,
                Vat = 21
            };

            // Act
            const string url = "api/hotels/1/rooms";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Create_Hotel_Room_Without_Price()
        {
            // Arrange
            var model = new RoomViewModel
            {
                Name = Utils.RandomString(256),
                Vat = 21
            };

            // Act
            const string url = "api/hotels/1/rooms";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Create_Hotel_Room_Without_Vat()
        {
            // Arrange
            var model = new RoomViewModel
            {
                Name = Utils.RandomString(256),
                Price = 30
            };

            // Act
            const string url = "api/hotels/1/rooms";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Create_Hotel_Room_With_Name_Max_Length()
        {
            // Arrange
            var model = new RoomViewModel()
            {
                Name = Utils.RandomString(257),
                Price = 30,
                Vat = 21
            };

            // Act
            const string url = "api/hotels/1/rooms";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Create_Hotel_Room_With_Duplicate_Name()
        {
            // Arrange
            var model = new RoomViewModel()
            {
                Name = Utils.RandomString(256),
                Price = 30,
                Vat = 21
            };

            // Act
            const string url = "api/hotels/1/rooms";

            await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}
