using API.Models;
using API.Clients;
using Microsoft.AspNetCore.Mvc;

namespace FootballWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SquadInfo : ControllerBase
    {
        private readonly ILogger<SquadInfo> _logger;
        public SquadInfo(ILogger<SquadInfo> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public GetSquad Squad(int team_id)
        {
            GetSquadClient client = new GetSquadClient();
            return client.GetSquadAsync(team_id).Result;
        }
    }
}