using Newtonsoft.Json;
using TelegramBot.Models;
using Telegram.BotAPI.AvailableMethods;
using TelegramBotClient = Telegram.BotAPI.TelegramBotClient;

namespace TelegramBot.Services
{
    public static class NewsService
    {
        public static async Task GetNewsInfo(TelegramBotClient client, long chatId)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://localhost:5015/News");

            string content = await response.Content.ReadAsStringAsync();
            var news = JsonConvert.DeserializeObject<GetNews>(content);

            if (news.Results != null && news.Results.Count > 0)
            {
                var topFiveNews = news.Results.Take(5).ToList();
                foreach (var article in topFiveNews)
                {
                    string newsDetails = $"Заголовок: {article.title}\n" +
                                         $"Опис: {article.description}\n" +
                                         $"Джерело: {article.source_url}\n\n" +
                                         $"{article.link}";

                    await client.SendMessageAsync(
                        chatId: chatId,
                        text: newsDetails
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