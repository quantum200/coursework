using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class GetTeam
    {
        public List<ResponseTeam> Response { get; set; }
        public GetTeam()
        {
            Response = new List<ResponseTeam>();
        }
    }

    public class ResponseTeam
    {
        public Team Team { get; set; }
        public Venue Venue { get; set; }
        public ResponseTeam()
        {
            Team = new Team();
            Venue = new Venue();
        }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Logo { get; set; }
    }

    public class Venue
    {
        public string Name { get; set; }
        public string City { get; set; }
        public int Capacity { get; set; }
        public string Image { get; set; }
    }
}