using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace IntegrationTest
{
    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly TestServer Server;

        public TestFixture()
        {
            var builder = new WebHostBuilder().UseStartup<TStartup>();
            Server = new TestServer(builder);
        }

        public void Dispose()
        {
            Server.Dispose();
        }
    }
}
