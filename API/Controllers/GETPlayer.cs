using Microsoft.AspNetCore.Mvc;

namespace FootballWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GETPlayer : ControllerBase
    {
        private readonly DataBase _dataBase;

        public GETPlayer(DataBase dataBase)
        {
            _dataBase = dataBase;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            var players = await _dataBase.GetAllPlayers();
            return Ok(players);
        }
    }
}