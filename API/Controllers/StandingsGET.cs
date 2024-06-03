using API.Clients;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FootballWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StandingsInfo : ControllerBase
    {
        private readonly ILogger<StandingsInfo> _logger;
        public StandingsInfo(ILogger<StandingsInfo> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public GetStandings Standings(int team_id)
        {
            GetStandingsClient client = new GetStandingsClient();
            return client.GetStandingsAsync(team_id).Result;
        }
    }
}