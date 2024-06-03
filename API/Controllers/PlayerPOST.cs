using Microsoft.AspNetCore.Mvc;
using API.Clients;
using API.Models;

namespace FootballWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerPOST : ControllerBase
    {
        private readonly DataBase _dataBase;
        private readonly GetPlayerClient _playerClient;

        public PlayerPOST(DataBase dataBase, GetPlayerClient playerClient)
        {
            _dataBase = dataBase;
            _playerClient = playerClient;
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer(int teamId, string playerName)
        {
            var playerData = await _playerClient.GetPlayerAsync(teamId, playerName);

            if (playerData.Response == null || playerData.Response.Count == 0)
            {
                return NotFound("Player not found.");
            }

            var responsePlayer = playerData.Response.First();

            var footballPlayer = new FootballPlayer
            {
                Firstname = responsePlayer.Player.Firstname,
                Lastname = responsePlayer.Player.Lastname,
                Age = responsePlayer.Player.Age,
                Height = responsePlayer.Player.Height,
                Weight = responsePlayer.Player.Weight,
                Photo = responsePlayer.Player.Photo,
                Club = responsePlayer.Statistics.FirstOrDefault()?.Team.Name,
                Country = responsePlayer.Statistics.FirstOrDefault()?.League.Country,
                LeagueName = responsePlayer.Statistics.FirstOrDefault()?.League.Name,
                LeagueCountry = responsePlayer.Statistics.FirstOrDefault()?.League.Country,
                GoalsTotal = responsePlayer.Statistics.FirstOrDefault()?.Goals.Total,
                GoalsAssists = responsePlayer.Statistics.FirstOrDefault()?.Goals.Assists,
                LeagueSeason = responsePlayer.Statistics.FirstOrDefault()?.League.Season,
                TeamName = responsePlayer.Statistics.FirstOrDefault()?.Team.Name,
                TeamLogo = responsePlayer.Statistics.FirstOrDefault()?.Team.Logo,
                LeagueLogo = responsePlayer.Statistics.FirstOrDefault()?.League.Logo,
                LeagueFlag = responsePlayer.Statistics.FirstOrDefault()?.League.Flag
            };

            await _dataBase.InsertFootballPlayer(footballPlayer);
            return Ok("Player added successfully.");
        }
    }
}