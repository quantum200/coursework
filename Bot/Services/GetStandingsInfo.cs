using Newtonsoft.Json;
using TelegramBot.Models;
using Telegram.BotAPI.AvailableMethods;
using TelegramBotClient = Telegram.BotAPI.TelegramBotClient;

namespace TelegramBot.Services
{
    public static class StandingsService
    {
        public static async Task GetStandingsInfo(TelegramBotClient client, long chatId, int team_Id)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"http://localhost:5015/StandingsInfo?team_id={team_Id}");
            string content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetStandings>(content);

            if (result.Response != null && result.Response.Any())
            {
                foreach (var responseStandings in result.Response)
                {
                    var leagueInfo = responseStandings.League;
                    var teamInfo = responseStandings.Team;

                    foreach (var standingsList in leagueInfo.Standings)
                    {
                        foreach (var standings in standingsList)
                        {
                            string standingsDetails = $"ID ліги: {leagueInfo.Id}\n" +
                                                      $"Ліга: {leagueInfo.Name}\n" +
                                                      $"Країна: {leagueInfo.Country}\n\n" +
                                                      $"ID команди: {standings.Team.Id}\n" +
                                                      $"Команда: {standings.Team.Name}\n\n" +
                                                      $"Позиція: {standings.Rank}\n" +
                                                      $"Очки: {standings.Points}\n" +
                                                      $"Голів різниця: {standings.GoalsDiff}\n" +
                                                      $"Група: {standings.Group}\n" +
                                                      $"Форма: {standings.Form}\n" +
                                                      $"Опис: {standings.Description}\n" +
                                                      $"Ігри: {standings.All.Played}\n" +
                                                      $"Виграш: {standings.All.Win}\n" +
                                                      $"Нічия: {standings.All.Draw}\n" +
                                                      $"Програш: {standings.All.Lose}\n\n";

                            if (!string.IsNullOrEmpty(leagueInfo.Logo))
                            {
                                await client.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: leagueInfo.Logo,
                                    caption: $"Логотип ліги: {leagueInfo.Name}"
                                );
                            }

                            if (!string.IsNullOrEmpty(standings.Team.Logo))
                            {
                                await client.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: standings.Team.Logo,
                                    caption: $"Логотип команди: {standings.Team.Name}"
                                );
                            }

                            await client.SendMessageAsync(
                                chatId: chatId,
                                text: standingsDetails
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