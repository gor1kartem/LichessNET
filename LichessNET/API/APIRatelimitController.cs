using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TokenBucket;
using Vertical.SpectreLogger;

namespace LichessNET.API
{
    public class APIRatelimitController
    {

        ITokenBucket defaultBucket = TokenBuckets.Construct().WithCapacity(5).WithFixedIntervalRefillStrategy(5, TimeSpan.FromSeconds(15)).Build();

        Dictionary<string, ITokenBucket> buckets = new Dictionary<string, ITokenBucket>();

        static DateTime RateLimitedUntil = DateTime.MinValue;

        static ILogger logger;

        public APIRatelimitController()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder
                .AddSpectreConsole());

            logger = loggerFactory.CreateLogger("APIRateLimitController");
        }

        public static void ReportBlock(int seconds = 60)
        {
            logger.LogWarning("API Call reported Ratelimit block for " + seconds + " seconds");
            RateLimitedUntil = DateTime.Now.AddSeconds(seconds);
        }

        public void RegisterBucket(string EndpointURL, ITokenBucket bucket)
        {
            buckets.Add(EndpointURL, bucket);
        }

        public void Consume(string EndpointURL, bool consumedefaultBucket)
        {

            if(RateLimitedUntil > DateTime.Now)
            {
                logger.LogWarning("Endpoint call to " + EndpointURL + " blocked due to ratelimit. Waiting for " + (RateLimitedUntil - DateTime.Now).Milliseconds + " ms.");
                Thread.Sleep((RateLimitedUntil - DateTime.Now).Milliseconds);
            }

            if(consumedefaultBucket) defaultBucket.Consume();
            if (buckets.ContainsKey(EndpointURL))
            {
                buckets[EndpointURL].Consume();
            }
        }


    }
}
