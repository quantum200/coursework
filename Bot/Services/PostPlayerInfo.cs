using Telegram.BotAPI.AvailableMethods;
using TelegramBotClient = Telegram.BotAPI.TelegramBotClient;

namespace TelegramBot.Services
{
    public static class PlayerPOST
    {
        public static async Task AddPlayerAsync(TelegramBotClient client, long chatId, int teamId, string playerName)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync($"http://localhost:5015/PlayerPOST?teamId={teamId}&playerName={playerName}", null);

            if (response.IsSuccessStatusCode)
            {
                await client.SendMessageAsync(
                    chatId: chatId,
                    text: "Інформація про нового гравця успішно додана у вихідні дані."
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