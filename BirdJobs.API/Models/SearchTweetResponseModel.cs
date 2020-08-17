using System;
using System.Collections.Generic;

namespace BirdJobs.API.Models
{
    public class SearchTweetResponseModel
    {
        public List<ResultsProperties> results { get; set; }
        public string next { get; set; }
        public RequestParameters requestParameters { get; set; }
    }

    public class ResultsProperties
    {
        public string created_at { get; set; }
        public string text { get; set; }
        public string source { get; set; }
        public bool truncated { get; set; }
        public UserDetails user { get; set; }
    }

    public class RequestParameters
    {
        public int maxResults { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }

    public class UserDetails
    {
        public string id_str { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string profile_background_color { get; set; }
        public string profile_image_url_https { get; set; }
    }
}