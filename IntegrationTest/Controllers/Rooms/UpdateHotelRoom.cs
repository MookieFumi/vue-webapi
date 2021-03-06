﻿using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTest.Infrastructure;
using Vuew;
using Vuew.ViewModels;
using Xunit;

namespace IntegrationTest.Controllers.Rooms
{
    public class UpdateHotelRoom :TestBase
    {
        public UpdateHotelRoom(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task Can_Update_Hotel_Room()
        {
            // Arrange
            var model = new RoomViewModel()
            {
                Name = Utils.RandomString(256),
                Price = 30,
                Vat = 21
            };

            // Act
            const string url = "api/hotels/1/rooms/1";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .SendAsync("PUT");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Can_Not_Update_Hotel_Room_Without_Name()
        {
            // Arrange
            var model = new RoomViewModel
            {
                Price = 30,
                Vat = 21
            };

            // Act
            const string url = "api/hotels/1/rooms/1";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .SendAsync("PUT");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Update_Hotel_Room_Without_Price()
        {
            // Arrange
            var model = new RoomViewModel
            {
                Name = Utils.RandomString(256),
                Vat = 21
            };

            // Act
            const string url = "api/hotels/1/rooms/1";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .SendAsync("PUT");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Update_Hotel_Room_Without_Vat()
        {
            // Arrange
            var model = new RoomViewModel
            {
                Name = Utils.RandomString(256),
                Price = 30
            };

            // Act
            const string url = "api/hotels/1/rooms/1";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .SendAsync("PUT");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Update_Hotel_Room_With_Name_Max_Length()
        {
            // Arrange
            var model = new RoomViewModel
            {
                Name = Utils.RandomString(257),
                Price = 30,
                Vat = 21
            };

            // Act
            const string url = "api/hotels/1/rooms/1";
            var response = await Server.CreateRequest(url)
                .WithContent(model)
                .SendAsync("PUT");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Can_Not_Update_Hotel_Room_With_Duplicate_Name()
        {
            // Arrange
            var model = new RoomViewModel
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

            const string url2 = "api/hotels/1/rooms/1";
            var response = await Server.CreateRequest(url2)
                .WithContent(model)
                .SendAsync("PUT");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}
