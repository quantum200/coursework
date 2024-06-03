using API.Clients;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FootballWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerInfo : ControllerBase
    {
        private readonly ILogger<PlayerInfo> _logger;
        public PlayerInfo(ILogger<PlayerInfo> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ResponsePlayer Player(int team_id, string PlayerName)
        {
            GetPlayerClient client = new GetPlayerClient();
            return client.GetPlayerAsync(team_id, PlayerName).Result.Response[0];
        }
    }
}