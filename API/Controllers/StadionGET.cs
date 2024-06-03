using API.Clients;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FootballWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StadionInfo : ControllerBase
    {
        private readonly ILogger<StadionInfo> _logger;
        public StadionInfo(ILogger<StadionInfo> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public GetStadion Stadion(string city_name)
        {
            GetStadionClient client = new GetStadionClient();
            return client.GetStadionAsync(city_name).Result;
        }
    }
}