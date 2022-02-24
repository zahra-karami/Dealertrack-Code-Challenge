using Polly;
using Polly.Retry;
namespace DTN.Logic.Services
{
    public class BaseService
    {
        protected RetryPolicy _retryPolicy;
        private protected BaseService()
        {
            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetry(2, retryAttempt => {
                    var timeToWait = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                    return timeToWait;
                }
                );
        }
    }
}
