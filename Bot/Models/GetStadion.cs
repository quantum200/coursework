using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class GetStadion
    {
        public List<Response> response { get; set; }
        public GetStadion()
        {
            response = new List<Response>();
        }
    }
    public class Response
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public int capacity { get; set; }
        public string surface { get; set; }
        public string image { get; set; }
    }
}