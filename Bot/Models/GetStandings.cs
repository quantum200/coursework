using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class GetStandings
    {
        public List<ResponseStandings> Response { get; set; }
        public GetStandings()
        {
            Response = new List<ResponseStandings>();
        }
    }

    public class ResponseStandings
    {
        public Team1 Team { get; set; }
        public League1 League { get; set; }
        public List<Standing> Standings { get; set; }
        public ResponseStandings()
        {
            Team = new Team1();
            League = new League1();
            Standings = new List<Standing>();
        }
    }

    public class Team1
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
    }

    public class Goals1
    {
        public int? Form { get; set; }
        public int? Against { get; set; }
    }

    public class AllStats
    {
        public int? Played { get; set; }
        public int? Win { get; set; }
        public int? Draw { get; set; }
        public int? Lose { get; set; }
        public Goals1 Goals { get; set; }
    }

    public class HomeStats
    {
        public int? Played { get; set; }
        public int? Win { get; set; }
        public int? Draw { get; set; }
        public int? Lose { get; set; }
        public Goals1 Goals { get; set; }
    }

    public class AwayStats
    {
        public int? Played { get; set; }
        public int? Win { get; set; }
        public int? Draw { get; set; }
        public int? Lose { get; set; }
        public Goals1 Goals { get; set; }
    }

    public class Standing
    {
        public int? Rank { get; set; }
        public Team1 Team { get; set; }
        public int? Points { get; set; }
        public int? GoalsDiff { get; set; }
        public string Group { get; set; }
        public string Form { get; set; }
        public string Description { get; set; }
        public AllStats All { get; set; }
        public HomeStats Home { get; set; }
        public AwayStats Away { get; set; }
    }

    public class League1
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Logo { get; set; }
        public string Flag { get; set; }
        public List<List<Standing>> Standings { get; set; }
    }
}