using API.Models;
using Newtonsoft.Json;

namespace API.Clients
{
    public class GetTeamClient
    {
        public async Task<GetTeam> GetTeamAsync(string TeamName)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://v3.football.api-sports.io/teams?name={TeamName}"),
                Headers = {
{ "x-rapidapi-key", "9bdff144fe0020adbed03d43da7755e4" },
    { "x-rapidapi-host", "v3.football.api-sports.io" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetTeam>(content);
                return result;
            }
        }
    }
}