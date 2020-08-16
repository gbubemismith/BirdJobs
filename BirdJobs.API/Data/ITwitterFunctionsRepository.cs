using System.Threading.Tasks;

namespace BirdJobs.API.Data
{
    public interface ITwitterFunctionsRepository
    {
         Task SearchTweets();
    }
}