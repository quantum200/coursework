using Newtonsoft.Json;
using Telegram.BotAPI.AvailableMethods;
using TelegramBot.Models;
using TelegramBotClient = Telegram.BotAPI.TelegramBotClient;

namespace TelegramBot.Services
{
    public static class StadionService
    {
        public static async Task GetStadionInfo(TelegramBotClient client, long chatId, string cityName)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"http://localhost:5015/StadionInfo?city_name={cityName}");
            string content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetStadion>(content);

            if (result.response != null && result.response.Count > 0)
            {
                foreach (var stadionInfo in result.response)
                {
                    string stadionDetails = $"Назва: {stadionInfo.name}\n" +
                                            $"Адреса: {stadionInfo.address}\n" +
                                            $"Місто: {stadionInfo.city}\n" +
                                            $"Країна: {stadionInfo.country}\n" +
                                            $"Місткість: {stadionInfo.capacity}\n" +
                                            $"Поверхня: {stadionInfo.surface}\n";

                    if (!string.IsNullOrEmpty(stadionInfo.image))
                    {
                        await client.SendPhotoAsync(
                            chatId: chatId,
                            photo: stadionInfo.image,
                            caption: stadionDetails
                        );
                    }
                    else
                    {
                        await client.SendMessageAsync(
                            chatId: chatId,
                            text: stadionDetails
                        );
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