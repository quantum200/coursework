using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using TelegramBot.Services;

namespace TelegramBot
{
    class Program
    {
        static async Task Main()
        {
            var client = new TelegramBotClient("7099779779:AAHoj223lj0-YMmAHV6d6dPznCoHL7teObU");
            var updates = client.GetUpdates();

            while (true)
            {
                if (updates.Any())
                {
                    foreach (var update in updates)
                    {
                        if (update.Message != null)
                        {
                            var chatId = update.Message.Chat.Id;
                            string keyword = update.Message.Text;

                            if (keyword.StartsWith("/team"))
                            {
                                var parts = keyword.Split(' ');
                                if (parts.Length >= 2 && parts[0] == "/team")
                                {
                                    string teamName = string.Join(' ', parts.Skip(1));
                                    await TeamService.GetTeamInfo(client, chatId, teamName);
                                }
                                else
                                {
                                    await client.SendMessageAsync(
                                        chatId: chatId,
                                        text: "Введіть такий формат команди: /team team_name"
                                    );
                                }
                            }
                            else if (keyword.StartsWith("/player"))
                            {
                                var parts = keyword.Split(' ');
                                if (parts.Length == 3)
                                {
                                    if (int.TryParse(parts[1], out int teamId))
                                    {
                                        string playerName = parts[2];
                                        await PlayerService.GetPlayerInfo(client, chatId, teamId, playerName);
                                    }
                                }
                                else
                                {
                                    await client.SendMessageAsync(
                                        chatId: chatId,
                                        text: "Введіть такий формат команди: /player team_id player_name. team_id береться з команди /team"
                                    );
                                }
                            }
                            else if (keyword.StartsWith("/standings"))
                            {
                                var parts = keyword.Split(' ');
                                if (parts.Length == 2)
                                {
                                    if (int.TryParse(parts[1], out int team_Id))
                                    {
                                        await StandingsService.GetStandingsInfo(client, chatId, team_Id);
                                    }
                                }
                                else
                                {
                                    await client.SendMessageAsync(
                                        chatId: chatId,
                                        text: "Введіть такий формат команди: /standings team_id. team_id береться з команди /team"
                                    );
                                }
                            }
                            else if (keyword.StartsWith("/squad"))
                            {
                                var parts = keyword.Split(' ');
                                if (parts.Length == 2)
                                {
                                    if (int.TryParse(parts[1], out int teamId))
                                    {
                                        await SquadService.GetSquadInfo(client, chatId, teamId);
                                    }
                                    else
                                    {
                                        await client.SendMessageAsync(
                                            chatId: chatId,
                                            text: "Введіть такий формат команди: /squad team_id. team_id береться з команди /team"
                                        );
                                    }
                                }
                                else
                                {
                                    await client.SendMessageAsync(
                                        chatId: chatId,
                                        text: "Введіть такий формат команди: /squad team_id. team_id береться з команди /team"
                                    );
                                }
                            }
                            else if (keyword.StartsWith("/stadion"))
                            {
                                var parts = keyword.Split(' ');
                                if (parts.Length >= 2 && parts[0] == "/stadion")
                                {
                                    string cityName = string.Join(' ', parts.Skip(1));
                                    await StadionService.GetStadionInfo(client, chatId, cityName);
                                }
                                else
                                {
                                    await client.SendMessageAsync(
                                        chatId: chatId,
                                        text: "Введіть такий формат команди: /stadion city_name"
                                    );
                                }
                            }
                            else if (keyword.StartsWith("/news"))
                            {
                                await NewsService.GetNewsInfo(client, chatId);
                            }
                            else if (keyword.StartsWith("/addplayer"))
                            {
                                var parts = keyword.Split(' ');
                                if (parts.Length == 3)
                                {
                                    if (int.TryParse(parts[1], out int teamId))
                                    {
                                        string playerName = parts[2];
                                        await PlayerPOST.AddPlayerAsync(client, chatId, teamId, playerName);
                                    }
                                }
                                else
                                {
                                    await client.SendMessageAsync(
                                        chatId: chatId,
                                        text: "Введіть такий формат команди: /addplayer team_id player_name"
                                    );
                                }
                            }
                            else if (keyword.StartsWith("/checkdatabase"))
                            {
                                await CheckPlayerGET.GetPlayersAsync(client, chatId);
                            }
                            else if (keyword.StartsWith("/cleardatabase"))
                            {
                                await DeletePlayerInfo.DeleteAllPlayersAsync(client, chatId);
                            }
                            else
                            {
                                await client.SendMessageAsync(
                                    chatId: chatId,
                                    text: "Я не знаю що ви хочете ввести, будь ласка напишіть правильно"
                                );
                            }
                        }
                    }

                    var offset = updates.Last().UpdateId + 1;
                    updates = client.GetUpdates(offset);
                }
                else
                {
                    updates = client.GetUpdates();
                }
            }
        }
    }
}