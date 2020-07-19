using System.Threading.Tasks;
using BirdJobs.API.Models;

namespace BirdJobs.API.Data
{
    public interface ITwitterAuthRepository
    {
        Task<RequestTokenResponse> GetRequestToken();
        Task GetAccessToken(string token, string oauthVerifier);
    }
}