using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTest.Infrastructure;
using Vuew;
using Vuew.ViewModels;
using Xunit;

namespace IntegrationTest.Controllers.Hotels
{
    public class GetHotelById : TestBase
    {
        public GetHotelById(TestFixture<Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task Can_Get_Hotel_By_Id()
        {
            // Act
            const string url = "api/hotels/1";
            var response = await Server.CreateRequest(url)
                .GetAsync();

            var value = await response.Content.ReadAsAsync<HotelViewModel>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            value.Id.Should().Be(1);
        }

        [Fact]
        public async Task Can_Not_Get_Hotel_By_Id_With_Not_Exist_Id()
        {
            // Act
            var url = $"api/hotels/{int.MaxValue}";
            var response = await Server.CreateRequest(url)
                .GetAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
