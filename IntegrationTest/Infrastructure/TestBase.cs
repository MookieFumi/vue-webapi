using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace IntegrationTest.Infrastructure
{
    public class TestBase : IClassFixture<TestFixture<Vuew.Startup>>
    {
        protected TestServer Server { get; }

        public TestBase(TestFixture<Vuew.Startup> fixture)
        {
            Server = fixture.Server;
        }
    }
}
