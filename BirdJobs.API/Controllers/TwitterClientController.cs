using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BirdJobs.API.Data;
using BirdJobs.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BirdJobs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterClientController : ControllerBase
    {
        private readonly ITwitterAuthRepository _twitterAuth;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly DataContext _context;
        public TwitterClientController(ITwitterAuthRepository twitterAuth, IMapper mapper, 
        IConfiguration config, DataContext context)
        {
            _context = context;
            _config = config;
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
            var details = await _twitterAuth.VerifyCredentials(response.Token, response.TokenSecret);


            var userCheck = await _context.Users.FirstOrDefaultAsync(i => i.UserId == int.Parse(details.id));


            if (userCheck is null)
            {
                var userToCreate = _mapper.Map<User>(response);

                _twitterAuth.Add<User>(userToCreate);

                await _twitterAuth.SaveAll();
            }


            //creating JWT token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, details.id),
                new Claim(ClaimTypes.Name, details.screen_name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                userDetails = details
            });


        }


        return BadRequest("Error getting access token and user details");


    }


    }   
}