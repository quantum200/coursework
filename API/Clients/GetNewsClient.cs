using API.Models;
using Newtonsoft.Json;

namespace API.Clients
{
    public class GetNewsClient
    {
        public async Task<GetNews> GetNewsAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://newsdata.io/api/1/news?apikey=pub_440269e1706bb82e453f9ce582d969533b29b&q=football&language=uk")
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetNews>(content);
                return result;
            }
        }
    }
}