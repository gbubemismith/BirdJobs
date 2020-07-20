namespace BirdJobs.API.Models
{
    public class AccessTokenResponse
    {
        public string oauth_token { get; set; }
        public string oauth_token_secret { get; set; }
        public string user_id { get; set; }
        public string screen_name { get; set; }
    }
}