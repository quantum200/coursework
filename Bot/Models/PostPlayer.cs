using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class PostPlayer
    {
        public List<ResponsePost> Response { get; set; }
        public PostPlayer()
        {
            Response = new List<ResponsePost>();
        }
    }
    public class ResponsePost
    {
        public PlayerPost Player { get; set; }
        public List<InfoPost> Info { get; set; }
        public ResponsePost()
        {
            Player = new PlayerPost();
            Info = new List<InfoPost>();
        }
    }
    public class PlayerPost
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
    }
    public class InfoPost
    {
        public TeamPost Team { get; set; }
        public CountryPost Country { get; set; }
    }
    public class TeamPost
    {
        public string Team { get; set; }
    }
    public class CountryPost
    {
        public string Counrty { get; set; }
    }
}