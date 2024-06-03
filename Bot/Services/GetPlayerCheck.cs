using Newtonsoft.Json;
using System.Text;
using Telegram.BotAPI.AvailableMethods;
using TelegramBot.Models;
using TelegramBotClient = Telegram.BotAPI.TelegramBotClient;

namespace TelegramBot.Services
{
    public static class CheckPlayerGET
    {
        public static async Task GetPlayersAsync(TelegramBotClient client, long chatId)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://localhost:5015/GETPlayer");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var players = JsonConvert.DeserializeObject<List<FootballPlayer>>(content);

                if (players.Count == 0)
                {
                    await client.SendMessageAsync(
                        chatId: chatId,
                        text: "Error"
                    );
                }
                else
                {
                    foreach (var player in players)
                    {
                        StringBuilder playerDetails = new StringBuilder();
                        playerDetails.AppendLine($"Ім'я: {player.Firstname}");
                        playerDetails.AppendLine($"Прізвище: {player.Lastname}");
                        playerDetails.AppendLine($"Вік: {player.Age}");
                        playerDetails.AppendLine($"Зріст: {player.Height}");
                        playerDetails.AppendLine($"Вага: {player.Weight}");
                        playerDetails.AppendLine($"Клуб: {player.Club}");
                        playerDetails.AppendLine($"Країна: {player.Country}");
                        playerDetails.AppendLine($"Назва ліги: {player.LeagueName}");
                        playerDetails.AppendLine($"Країна ліги: {player.LeagueCountry}");
                        playerDetails.AppendLine($"Сезон ліги: {player.LeagueSeason}");
                        playerDetails.AppendLine($"Голи: {player.GoalsTotal}");
                        playerDetails.AppendLine($"Гольові передачі: {player.GoalsAssists}");
                        playerDetails.AppendLine($"Назва команди: {player.TeamName}");

                        if (!string.IsNullOrEmpty(player.Photo))
                        {
                            await client.SendPhotoAsync(
                                chatId: chatId,
                                photo: player.Photo,
                                caption: playerDetails.ToString()
                            );
                        }
                        else
                        {
                            await client.SendMessageAsync(
                                chatId: chatId,
                                text: playerDetails.ToString()
                            );
                        }
                    }
                }
            }
            else
            {
                await client.SendMessageAsync(
                    chatId: chatId,
                    text: "Error"
                );
            }
        }
    }
}