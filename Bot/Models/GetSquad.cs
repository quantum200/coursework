namespace TelegramBot.Models
{
    public class GetSquad
    {
        public List<Response2> response { get; set; }
    }
    public class Player2
    {
        public string name { get; set; }
        public int age { get; set; }
        public int? number { get; set; }
        public string position { get; set; }
        public string photo { get; set; }
    }
    public class Response2
    {
        public Team2 team { get; set; }
        public List<Player2> players { get; set; }
    }

    public class Team2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
    }
}