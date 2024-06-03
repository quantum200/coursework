using Telegram.BotAPI.AvailableMethods;
using TelegramBotClient = Telegram.BotAPI.TelegramBotClient;

namespace TelegramBot.Services
{
    public static class DeletePlayerInfo
    {
        public static async Task DeleteAllPlayersAsync(TelegramBotClient client, long chatId)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync("http://localhost:5015/PlayerDELETE");

            if (response.IsSuccessStatusCode)
            {
                await client.SendMessageAsync(
                    chatId: chatId,
                    text: "База даних чиста."
                );
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