using API.Clients;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FootballWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamInfo : ControllerBase
    {
        private readonly ILogger<TeamInfo> _logger;
        public TeamInfo(ILogger<TeamInfo> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public GetTeam Team(string FootballClub)
        {
            GetTeamClient client = new GetTeamClient();
            return client.GetTeamAsync(FootballClub).Result;
        }
    }
}