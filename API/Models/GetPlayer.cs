namespace API.Models
{
    public class GetPlayer
    {
        public List<ResponsePlayer> Response { get; set; }
        public GetPlayer()
        {
            Response = new List<ResponsePlayer>();
        }
    }

    public class ResponsePlayer
    {
        public Player1 Player { get; set; }
        public List<Statistics> Statistics { get; set; }
        public ResponsePlayer()
        {
            Statistics = new List<Statistics>();
            Player = new Player1();
        }
    }
    public class FootballPlayer
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Photo { get; set; }
        public string Club { get; set; }
        public string Country { get; set; }
        public string LeagueName { get; set; }
        public string LeagueCountry { get; set; }
        public int? LeagueSeason { get; set; }
        public int? GoalsTotal { get; set; }
        public int? GoalsAssists { get; set; }
        public string TeamName { get; set; }
        public string TeamLogo { get; set; }
        public string LeagueLogo { get; set; }
        public string LeagueFlag { get; set; }
    }

    public class Player1
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Photo { get; set; }
    }

    public class Statistics
    {
        public TeamPlayer Team { get; set; }
        public League League { get; set; }
        public Goals Goals { get; set; }
    }

    public class TeamPlayer
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
    }

    public class League
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Logo { get; set; }
        public string Flag { get; set; }
        public int? Season { get; set; }
    }

    public class Goals
    {
        public int? Total { get; set; }
        public int? Assists { get; set; }
    }
}