using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BirdJobs.API.Dtos;
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
        private readonly DataContext _context;

        public TwitterAuthRepository(IConfiguration config, IHttpClientFactory clientFactory, IOptions<TwitterSettings> twitterConfig, DataContext context)
        {
            _context = context;
            _twitterConfig = twitterConfig;
            _clientFactory = clientFactory;
            _config = config;

        }

        //Get Request Token
        public async Task<RequestTokenResponse> GetRequestToken()
        {

            var requestTokenResponse = new RequestTokenResponse();


            var client = _clientFactory.CreateClient("twitter");
            var consumerKey = _twitterConfig.Value.AppId;
            var consumerSecret = _twitterConfig.Value.AppSecret;
            var callbackUrl = "http://localhost:4200/login";

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


                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return requestTokenResponse;

        }

        //Get Access Token
        public async Task<UserModelDto> GetAccessToken(string token, string oauthVerifier)
        {
            var client = _clientFactory.CreateClient("twitter");
            var consumerKey = _twitterConfig.Value.AppId;
            var consumerSecret = _twitterConfig.Value.AppSecret;

            var accessTokenResponse = new UserModelDto();

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

           
            try
            {
                var content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string, string>("oauth_verifier", oauthVerifier)
                });

                using (var response = await client.PostAsync(oauthClient.RequestUrl, content))
                {
                    response.EnsureSuccessStatusCode();

                    //twiiter responds with a string concatenated by &
                    var responseString = response.Content.ReadAsStringAsync()
                                               .Result.Split("&");

                    //split by = to get acual values
                    accessTokenResponse = new UserModelDto 
                    {
                        Token = responseString[0].Split("=")[1],
                        TokenSecret = responseString[1].Split("=")[1],
                        UserId = responseString[2].Split("=")[1],
                        Username = responseString[3].Split("=")[1]
                    };
                    
                }
            }
            catch (Exception ex)
            {

                
            }

            return accessTokenResponse;
        }

        //Get User Details
        public async Task<TwitterUserDetails> VerifyCredentials(string token, string tokenSecret)
        {
            var client = _clientFactory.CreateClient("twitter");
            var consumerKey = _twitterConfig.Value.AppId;
            var consumerSecret = _twitterConfig.Value.AppSecret;

            client.DefaultRequestHeaders.Accept.Clear();

            var userDetails = new TwitterUserDetails();

            var oauthClient = new OAuthRequest 
            {
                Method = "GET",
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                Token = token,
                TokenSecret = tokenSecret,
                RequestUrl = "https://api.twitter.com/1.1/account/verify_credentials.json",
                Version = "1.0a",
                Realm = "twitter.com"
            };

            string auth = oauthClient.GetAuthorizationHeader();

            client.DefaultRequestHeaders.Add("Authorization", auth);

            try
            {
                using (var response = await client.GetAsync(oauthClient.RequestUrl))
                {
                    response.EnsureSuccessStatusCode();

                    userDetails = JsonConvert.DeserializeObject<TwitterUserDetails>(response.Content.     ReadAsStringAsync().Result);

                }
            }
            catch (Exception ex)
            {
                
                
            }

            return userDetails;

        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

       
    }
}