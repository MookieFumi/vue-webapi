using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTest.Infrastructure;
using VueWebApi;
using VueWebApi.ViewModels;
using Xunit;

namespace IntegrationTest.Controllers.Hotels
{
    public class UpdateHotel : TestBase
    {
        public UpdateHotel(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task Can_Update_Hotel()
        {
            // Arrange
            var model = new HotelViewModel
            {
                Name = Utils.RandomString(256),
                City = "Brihuega"
            };

            // Act
            const string url = "api/hotels/1";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .SendAsync("PUT");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Can_Not_Update_Hotel_Without_Name()
        {
            // Arrange
            var model = new HotelViewModel
            {
                City = "Brihuega"
            };

            // Act
            const string url = "api/hotels/1";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .SendAsync("PUT");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Update_Hotel_Without_City()
        {
            // Arrange
            var model = new HotelViewModel
            {
                Name = Utils.RandomString(256)
            };

            // Act
            const string url = "api/hotels/1";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .SendAsync("PUT");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Update_Hotel_With_Name_Max_Length()
        {
            // Arrange
            var model = new HotelViewModel
            {
                Name = Utils.RandomString(257),
                City = "Brihuega"
            };

            // Act
            const string url = "api/hotels/1";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .SendAsync("PUT");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Update_Hotel_With_City_Max_Length()
        {
            // Arrange
            var model = new HotelViewModel
            {
                Name = Utils.RandomString(256),
                City = Utils.RandomString(257)
            };

            // Act
            const string url = "api/hotels/1";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .SendAsync("PUT");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Update_Not_Exist_Hotel()
        {
            // Arrange
            var model = new HotelViewModel
            {
                Name = Utils.RandomString(256),
                City = "Brihuega"
            };

            // Act
            var url = $"api/hotels/{int.MaxValue}";

            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Can_Not_Update_Hotel_With_Duplicate_Name()
        {
            // Arrange
            var model = new HotelViewModel
            {
                Name = Utils.RandomString(256),
                City = "Brihuega"
            };

            // Act
            const string url = "api/hotels";

            await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            const string url2 = "api/hotels/1";

            var response = await Server.CreateRequest(url2)
                .WithContent(model)
                .SendAsync("PUT");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}
