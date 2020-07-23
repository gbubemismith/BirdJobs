using System;

namespace BirdJobs.API.Dtos
{
    public class UserModelDto
    {
        public string Username { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string TokenSecret { get; set; }
        
    }
}