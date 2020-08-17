namespace BirdJobs.API.Models
{
    public class SearchTweetRequestModel
    {
        public string query { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string maxResults { get; set; }
        public string next { get; set; }
    }
}