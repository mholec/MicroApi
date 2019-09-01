using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroApi.Services
{
    public class DownloadService
    {
        public async Task<string> LoadFromUrl(string url)
        {
            using var client = new HttpClient();
            using var requestmessage = new HttpRequestMessage(new HttpMethod("GET"), url);
            requestmessage.Version = new Version(2, 0);
            using var response = await client.SendAsync(requestmessage);
            return await response.Content.ReadAsStringAsync();
        }
    }
}