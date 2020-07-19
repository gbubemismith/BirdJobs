using System.Threading.Tasks;
using BirdJobs.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace BirdJobs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterClientController : ControllerBase
    {
        private readonly ITwitterAuthRepository _twitterAuth;
        public TwitterClientController(ITwitterAuthRepository twitterAuth)
        {
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
            

            await _twitterAuth.GetAccessToken(oauth_token, oauth_verifier);

            return Ok();
        }
    }
}