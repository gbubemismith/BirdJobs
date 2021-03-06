using System.Threading.Tasks;
using BirdJobs.API.Dtos;
using BirdJobs.API.Models;

namespace BirdJobs.API.Data
{
    public interface ITwitterAuthRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<RequestTokenResponse> GetRequestToken();
        Task<UserModelDto> GetAccessToken(string token, string oauthVerifier);
        Task<TwitterUserDetails> VerifyCredentials(string token, string tokenSecret);
    }
}