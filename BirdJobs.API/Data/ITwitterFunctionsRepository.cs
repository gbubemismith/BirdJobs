using System.Threading.Tasks;
using BirdJobs.API.Models;

namespace BirdJobs.API.Data
{
    public interface ITwitterFunctionsRepository
    {
        Task<SearchTweetResponseModel> SearchTweets(SearchTweetRequestModel requestModel);
    }
}