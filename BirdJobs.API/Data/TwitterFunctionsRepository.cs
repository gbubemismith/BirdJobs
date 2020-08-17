using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BirdJobs.API.Helpers;
using BirdJobs.API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OAuth;

namespace BirdJobs.API.Data
{
    public class TwitterFunctionsRepository : ITwitterFunctionsRepository
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IOptions<TwitterSettings> _twitterConfig;
        public TwitterFunctionsRepository(IConfiguration config, IHttpClientFactory clientFactory, IOptions<TwitterSettings> twitterConfig)
        {
            _twitterConfig = twitterConfig;
            _clientFactory = clientFactory;
            _config = config;

        }
        public async Task<SearchTweetResponseModel> SearchTweets()
        {
            var client = _clientFactory.CreateClient("twitter");
            var consumerKey = _twitterConfig.Value.AppId;
            var consumerSecret = _twitterConfig.Value.AppSecret;

            var tweetsResponse = new SearchTweetResponseModel();

            client.DefaultRequestHeaders.Accept.Clear();

            // var req = new  
            // {
            //     query = "(#hiring (#developer OR software engineer OR software developer OR #tech)) has:links lang:en"
            // };

            var req = new SearchTweetRequestModel
            {
                query = "(#hiring (#developer OR software engineer OR software developer OR #tech)) has:links lang:en"
            };

            var json = JsonSerializer.Serialize(req, new JsonSerializerOptions { IgnoreNullValues = true });

            var reqContent = new StringContent(json, Encoding.UTF8, "application/json");

          
            var oauthClient = new OAuthRequest
            {
                Method = "POST",
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                RequestUrl = "https://api.twitter.com/1.1/tweets/search/30day/dev.json",
                Version = "1.0a",
                Realm = "twitter.com"
            };

            string auth = oauthClient.GetAuthorizationHeader();

            client.DefaultRequestHeaders.Add("Authorization", auth);

            try
            {
                using var httpResponse = await client.PostAsync(oauthClient.RequestUrl, reqContent);

                //httpResponse.EnsureSuccessStatusCode();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                    tweetsResponse = await JsonSerializer.DeserializeAsync<SearchTweetResponseModel>(contentStream);
                }

                var check = await httpResponse.Content.ReadAsStringAsync();

                
            }
            catch (Exception ex)
            {
                
                throw;
            }

            return tweetsResponse;
        }

        
    }
}