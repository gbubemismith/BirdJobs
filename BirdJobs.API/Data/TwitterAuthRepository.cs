using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BirdJobs.API.Helpers;
using BirdJobs.API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OAuth;

namespace BirdJobs.API.Data
{
    public class TwitterAuthRepository : ITwitterAuthRepository
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IOptions<TwitterSettings> _twitterConfig;

        public TwitterAuthRepository(IConfiguration config, IHttpClientFactory clientFactory, IOptions<TwitterSettings> twitterConfig)
        {
            _twitterConfig = twitterConfig;
            _clientFactory = clientFactory;
            _config = config;

        }


        public async Task<RequestTokenResponse> GetRequestToken()
        {

            var requestTokenResponse = new RequestTokenResponse();


            var client = _clientFactory.CreateClient("twitter");
            var consumerKey = _twitterConfig.Value.AppId;
            var consumerSecret = _twitterConfig.Value.AppSecret;
            var callbackUrl = "http://localhost:5000/api/twitterclient/sign-in-with-twitter";

            client.DefaultRequestHeaders.Accept.Clear();

            var oauthClient = new OAuthRequest
            {
                Method = "POST",
                Type = OAuthRequestType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                RequestUrl = "https://api.twitter.com/oauth/request_token",
                Version = "1.0a",
                Realm = "twitter.com",
                CallbackUrl = callbackUrl
            };

            string auth = oauthClient.GetAuthorizationHeader();

            client.DefaultRequestHeaders.Add("Authorization", auth);



            try
            {
                var content = new StringContent("", Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync(oauthClient.RequestUrl, content))
                {
                    response.EnsureSuccessStatusCode();

                    var responseString = response.Content.ReadAsStringAsync()
                                               .Result.Split("&");


                    requestTokenResponse = new RequestTokenResponse
                    {
                        oauth_token = responseString[0],
                        oauth_token_secret = responseString[1],
                        oauth_callback_confirmed = responseString[2]
                    };

                    // var check = JsonConvert.DeserializeObject<Object>(response.Content.ReadAsStringAsync().Result);

                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return requestTokenResponse;

        }

        public async Task GetAccessToken(string token, string oauthVerifier)
        {
            var client = _clientFactory.CreateClient("twitter");
            var consumerKey = _twitterConfig.Value.AppId;
            var consumerSecret = _twitterConfig.Value.AppSecret;

            client.DefaultRequestHeaders.Accept.Clear();

            var oauthClient = new OAuthRequest
            {
                Method = "POST",
                Type = OAuthRequestType.AccessToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                RequestUrl = "https://api.twitter.com/oauth/access_token",
                Token = token,
                Version = "1.0a",
                Realm = "twitter.com"
            };

            string auth = oauthClient.GetAuthorizationHeader();

            client.DefaultRequestHeaders.Add("Authorization", auth);

            var data = new
            {
                oauth_verifier = oauthVerifier
            };

            try
            {
                var content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string, string>("oauth_verifier", oauthVerifier)
                });

                using (var response = await client.PostAsync(oauthClient.RequestUrl, content))
                {
                    response.EnsureSuccessStatusCode();

                    var responseString = response.Content.ReadAsStringAsync()
                                               .Result.Split("&");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}