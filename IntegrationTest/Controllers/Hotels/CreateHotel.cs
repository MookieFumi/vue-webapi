using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTest.Infrastructure;
using VueWebApi;
using VueWebApi.ViewModels;
using Xunit;

namespace IntegrationTest.Controllers.Hotels
{
    public class CreateHotel : TestBase
    {
        public CreateHotel(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task Can_Create_Hotel()
        {
            // Arrange
            var model = new HotelViewModel
            {
                Name = Utils.RandomString(256),
                City = "Brihuega"
            };

            // Act
            const string url = "api/hotels";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Can_Not_Create_Hotel_Without_Name()
        {
            // Arrange
            var model = new HotelViewModel
            {
                City = "Brihuega"
            };

            // Act
            const string url = "api/hotels";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Create_Hotel_Without_City()
        {
            // Arrange
            var model = new HotelViewModel
            {
                Name = Utils.RandomString(256)
            };

            // Act
            const string url = "api/hotels";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Create_Hotel_With_Name_Max_Length()
        {
            // Arrange
            var model = new HotelViewModel
            {
                Name = Utils.RandomString(257),
                City = "Brihuega"
            };

            // Act
            const string url = "api/hotels";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Create_Hotel_With_City_Max_Length()
        {
            // Arrange
            var model = new HotelViewModel
            {
                Name = Utils.RandomString(256),
                City = Utils.RandomString(257)
            };

            // Act
            const string url = "api/hotels";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task Can_Not_Create_Hotel_With_Duplicate_Name()
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

            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .PostAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}
