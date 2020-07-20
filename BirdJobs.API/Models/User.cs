namespace BirdJobs.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string TokenSecret { get; set; }
        public string CreateDt { get; set; }
    }
}