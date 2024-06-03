using Newtonsoft.Json;
using TelegramBot.Models;
using Telegram.BotAPI.AvailableMethods;
using TelegramBotClient = Telegram.BotAPI.TelegramBotClient;

namespace TelegramBot.Services
{
    public static class SquadService
    {
        public static async Task GetSquadInfo(TelegramBotClient client, long chatId, int team_Id)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"http://localhost:5015/SquadInfo?team_id={team_Id}");
            string content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetSquad>(content);

            if (result.response != null && result.response.Any())
            {
                foreach (var response2 in result.response)
                {
                    var teamInfo = response2.team;

                    if (!string.IsNullOrEmpty(teamInfo.logo))
                    {
                        try
                        {
                            await client.SendPhotoAsync(
                                chatId: chatId,
                                photo: teamInfo.logo,
                                caption: $"Логотип команди: {teamInfo.name}"
                            );
                        }
                        catch (Exception ex)
                        {
                            await client.SendMessageAsync(
                                chatId: chatId,
                                text: $"Error: {ex.Message}"
                            );
                        }
                    }

                    foreach (var player in response2.players)
                    {
                        string playerDetails = $"Ім'я гравця: {player.name}\n" +
                                               $"Вік: {player.age}\n" +
                                               $"Номер: {player.number}\n" +
                                               $"Позиція: {player.position}";

                        if (!string.IsNullOrEmpty(player.photo))
                        {
                            try
                            {
                                await client.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: player.photo,
                                    caption: playerDetails
                                );
                            }
                            catch (Exception ex)
                            {
                                await client.SendMessageAsync(
                                    chatId: chatId,
                                    text: $"Error {player.name}: {ex.Message}"
                                );
                            }
                        }
                        else
                        {
                            await client.SendMessageAsync(
                                chatId: chatId,
                                text: $"{playerDetails}\nNo Photo"
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