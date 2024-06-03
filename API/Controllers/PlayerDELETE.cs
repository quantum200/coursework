using Microsoft.AspNetCore.Mvc;

namespace FootballWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerDELETE : ControllerBase
    {
        private readonly DataBase _dataBase;

        public PlayerDELETE(DataBase dataBase)
        {
            _dataBase = dataBase;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllPlayers()
        {
            await _dataBase.DeleteAllPlayers();
            return Ok("All players have been deleted.");
        }
    }
}