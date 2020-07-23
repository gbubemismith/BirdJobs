using System.Threading.Tasks;
using AutoMapper;
using BirdJobs.API.Data;
using BirdJobs.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdJobs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterClientController : ControllerBase
    {
        private readonly ITwitterAuthRepository _twitterAuth;
        private readonly IMapper _mapper;
        public TwitterClientController(ITwitterAuthRepository twitterAuth, IMapper mapper)
        {
            _mapper = mapper;
            _twitterAuth = twitterAuth;

        }

        [HttpGet("GetRequestToken")]
        public async Task<IActionResult> GetRequestToken()
        {
            //STEP 1 call made to /oauth/request_token
            var response = await _twitterAuth.GetRequestToken();

            return Ok(response);

        }

        [HttpGet("sign-in-with-twitter")]
        public async Task<IActionResult> SignInWithTwitter(string oauth_token, string oauth_verifier)
        {

            var response = await _twitterAuth.GetAccessToken(oauth_token, oauth_verifier);

            if (response.TokenSecret != null)
            {
                await _twitterAuth.VerifyCredentials(response.Token, response.TokenSecret);

                var userToCreate = _mapper.Map<User>(response);

                _twitterAuth.Add<User>(userToCreate);

                if (await _twitterAuth.SaveAll())
                    return Ok(response);

            }


            return BadRequest("Error getting access token");



        }

        [HttpGet("GetUserData")]
        public async Task<IActionResult> GetUserData()
        {


            return Ok();
        }
    }
}