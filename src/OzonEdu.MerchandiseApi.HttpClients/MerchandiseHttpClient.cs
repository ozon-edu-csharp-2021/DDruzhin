using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Models;

namespace OzonEdu.MerchandiseApi.HttpClients
{
    public interface IMerchandiseHttpClient
    {
        Task<MerchPackResponse> RequestMerch(long workerId, int merchType, CancellationToken token);
        Task<IEnumerable<MerchPackResponse>> RequestMerchInfo(long workerId, CancellationToken token);
    }

    public class MerchandiseHttpClient : IMerchandiseHttpClient
    {
        private readonly HttpClient _httpClient;

        public MerchandiseHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MerchPackResponse> RequestMerch(long workerId, int merchType, CancellationToken token)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("workerId", workerId.ToString()),
                new KeyValuePair<string, string>("merchType", merchType.ToString())
            });
            using var response = await _httpClient.PostAsync("v1/api/merchandise", content, token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<MerchPackResponse>(body);
        }

        public async Task<IEnumerable<MerchPackResponse>> RequestMerchInfo(long workerId, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"v1/api/merchandise{workerId}", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<IEnumerable<MerchPackResponse>>(body);
        }
    }
}