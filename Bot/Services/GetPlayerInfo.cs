using Newtonsoft.Json;
using Telegram.BotAPI.AvailableMethods;
using TelegramBot.Models;
using TelegramBotClient = Telegram.BotAPI.TelegramBotClient;

namespace TelegramBot.Services
{
    public static class PlayerService
    {
        public static async Task GetPlayerInfo(TelegramBotClient client, long chatId, int teamId, string playerName)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"http://localhost:5015/PlayerInfo?team_id={teamId}&PlayerName={playerName}");
            string content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponsePlayer>(content);

            var result1 = ConvertToGetPlayer(result);

            if (result1.Response != null && result1.Response.Count > 0)
            {
                var player = result1.Response.First().FootballPlayer;

                string playerDetails = $"Ім'я: {player.Firstname} {player.Lastname}\n" +
                                       $"Вік: {player.Age}\n" +
                                       $"Зріст: {player.Height}\n" +
                                       $"Вага: {player.Weight}\n" +
                                       $"Фото: {player.Photo}\n" +
                                       $"Клуб: {player.Club}\n" +
                                       $"Країна: {player.Country}\n" +
                                       $"Ліга: {player.LeagueName}\n" +
                                       $"Голи всього: {player.GoalsTotal}\n" +
                                       $"Голи асисти: {player.GoalsAssists}";

                if (!string.IsNullOrEmpty(player.Photo))
                {
                    await client.SendPhotoAsync(
                        chatId: chatId,
                        photo: player.Photo,
                        caption: playerDetails
                    );
                }
                else
                {
                    await client.SendMessageAsync(
                        chatId: chatId,
                        text: playerDetails
                    );
                }
            }
            else
            {
                await client.SendMessageAsync(
                    chatId: chatId,
                    text: "Інформація про гравця недоступна."
                );
            }
        }

        private static GetPlayer ConvertToGetPlayer(ResponsePlayer result)
        {
            return new GetPlayer
            {
                Response = new List<ResponsePlayer>
                {
                    new ResponsePlayer
                    {
                        Player = result.Player,
                        FootballPlayer = new FootballPlayer
                        {
                            Firstname = result.Player.Firstname,
                            Lastname = result.Player.Lastname,
                            Age = result.Player.Age ?? 0,
                            Height = result.Player.Height,
                            Weight = result.Player.Weight,
                            Photo = result.Player.Photo,
                            Club = result.Statistics.FirstOrDefault()?.Team.Name,
                            Country = result.Statistics.FirstOrDefault()?.League.Country,
                            LeagueName = result.Statistics.FirstOrDefault()?.League.Name,
                            LeagueCountry = result.Statistics.FirstOrDefault()?.League.Country,
                            GoalsTotal = result.Statistics.Sum(stat => stat.Goals.Total),
                            GoalsAssists = result.Statistics.Sum(stat => stat.Goals.Assists),
                            TeamName = result.Statistics.FirstOrDefault()?.Team.Name,
                            LeagueLogo = result.Statistics.FirstOrDefault()?.League.Logo,
                            LeagueFlag = result.Statistics.FirstOrDefault()?.League.Flag
                        },
                    }
                }
            };
        }
    }
}