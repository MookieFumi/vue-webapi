using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace IntegrationTest.Infrastructure
{
    public class TestBase : IClassFixture<TestFixture<VueWebApi.Startup>>
    {
        protected TestServer Server { get; }

        public TestBase(TestFixture<VueWebApi.Startup> fixture)
        {
            Server = fixture.Server;
        }
    }
}
