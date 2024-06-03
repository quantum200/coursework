using API.Clients;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FootballWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class News : ControllerBase
    {
        private readonly ILogger<News> _logger;
        public News(ILogger<News> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public GetNews NewsRequest()
        {
            GetNewsClient client = new GetNewsClient();
            return client.GetNewsAsync().Result;
        }
    }
}