using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationTest
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent httpContent)
        {
            var data = JsonConvert.DeserializeObject(await httpContent.ReadAsStringAsync(), typeof(T));

            return (T)data;
        }
    }
}
