using Newtonsoft.Json;
using TelegramBot.Models;
using Telegram.BotAPI.AvailableMethods;
using TelegramBotClient = Telegram.BotAPI.TelegramBotClient;

namespace TelegramBot.Services
{
    public static class TeamService
    {
        public static async Task GetTeamInfo(TelegramBotClient client, long chatId, string teamName)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"http://localhost:5015/TeamInfo?FootballClub={teamName}");
            string content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetTeam>(content);

            if (result.Response != null && result.Response.Count > 0)
            {
                var teamInfo = result.Response[0].Team;
                var venueInfo = result.Response[0].Venue;

                string teamDetails = $"Команда: {teamInfo.Name}\n" +
                                     $"Країна: {teamInfo.Country}\n";

                if (venueInfo != null)
                {
                    teamDetails += $"Стадіон: {venueInfo.Name}\n" +
                                   $"Місто: {venueInfo.City}\n" +
                                   $"Місткість: {venueInfo.Capacity}\n" +
                                   $"ID команди: {teamInfo.Id}";
                }

                if (!string.IsNullOrEmpty(teamInfo.Logo))
                {
                    await client.SendPhotoAsync(
                        chatId: chatId,
                        photo: teamInfo.Logo,
                        caption: teamDetails
                    );
                }
                else
                {
                    await client.SendMessageAsync(
                        chatId: chatId,
                        text: teamDetails
                    );
                }

                if (venueInfo != null && !string.IsNullOrEmpty(venueInfo.Image))
                {
                    await client.SendPhotoAsync(
                        chatId: chatId,
                        photo: venueInfo.Image,
                        caption: "Стадіон"
                    );
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