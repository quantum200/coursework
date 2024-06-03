namespace API.Models
{
    public class GetNews
    {
        public List<Results> Results { get; set; }
        public GetNews()
        {
            Results = new List<Results>();
        }
    }
    public class Results
    {
        public string title { get; set; }
        public string link { get; set; }
        public string video_url { get; set; }
        public string description { get; set; }
        public string image_url { get; set; }
        public string source_url { get; set; }
        public string source_icon { get; set; }
        public string language { get; set; }
    }
}