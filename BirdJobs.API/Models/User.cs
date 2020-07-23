using System;

namespace BirdJobs.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string TokenSecret { get; set; }
        public DateTime CreateDt { get; set; } = DateTime.Now;
    }
}