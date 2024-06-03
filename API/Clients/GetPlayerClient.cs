using Newtonsoft.Json;
using API.Models;

namespace API.Clients
{
    public class GetPlayerClient
    {
        public async Task<GetPlayer> GetPlayerAsync(int team_id, string PlayerName)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://v3.football.api-sports.io/players?team={team_id}&search={PlayerName}&season=2022"),
                Headers = {
                    { "x-rapidapi-key", "9bdff144fe0020adbed03d43da7755e4" },
                    { "x-rapidapi-host", "v3.football.api-sports.io" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetPlayer>(content);
                return result;
            }
        }
    }
}