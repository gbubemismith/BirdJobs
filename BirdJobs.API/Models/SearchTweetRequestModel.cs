namespace BirdJobs.API.Models
{
    public class SearchTweetRequestModel
    {
        public string query { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string maxResults { get; set; } = "60";
        public string next { get; set; }
    }
}