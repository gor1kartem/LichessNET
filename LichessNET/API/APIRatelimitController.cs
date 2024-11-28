using Microsoft.Extensions.Logging;
using TokenBucket;
using Vertical.SpectreLogger;

namespace LichessNET.API;

public class APIRatelimitController
{
    private static ILogger logger;

    private readonly Dictionary<string, ITokenBucket> buckets = new();

    private readonly ITokenBucket defaultBucket = TokenBuckets.Construct().WithCapacity(5)
        .WithFixedIntervalRefillStrategy(5, TimeSpan.FromSeconds(15)).Build();

    private int _pipedRequests;

    private DateTime RateLimitedUntil = DateTime.MinValue;

    public APIRatelimitController()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder
            .AddSpectreConsole());

        logger = loggerFactory.CreateLogger("APIRateLimitController");
    }

    public int PipedRequests
    {
        get => _pipedRequests;
        internal set
        {
            _pipedRequests = value;
            if (PipedRequests > 5)
                logger.LogWarning(
                    $"Currently there are {PipedRequests} requests in queue. Either the API is blocking requests, or the client is sending too many requests.");
        }
    }

    public void ReportBlock(int seconds = 60)
    {
        logger.LogWarning("API Call reported Ratelimit block for " + seconds + " seconds");
        RateLimitedUntil = DateTime.Now.AddSeconds(seconds);
    }

    public void RegisterBucket(string EndpointURL, ITokenBucket bucket)
    {
        buckets.Add(EndpointURL, bucket);
    }

    public void Consume()
    {
        PipedRequests++;
        if (RateLimitedUntil > DateTime.Now)
        {
            logger.LogWarning("Endpoint blocked due to ratelimit. Waiting for " +
                              (RateLimitedUntil - DateTime.Now).Milliseconds + " ms.");
            Thread.Sleep((RateLimitedUntil - DateTime.Now).Milliseconds);
        }

        defaultBucket.Consume();
        PipedRequests--;
    }

    public void Consume(string EndpointURL, bool consumedefaultBucket)
    {
        PipedRequests++;
        if (RateLimitedUntil > DateTime.Now)
        {
            logger.LogWarning("Endpoint call to " + EndpointURL + " blocked due to ratelimit. Waiting for " +
                              (RateLimitedUntil - DateTime.Now).Milliseconds + " ms.");
            Thread.Sleep((RateLimitedUntil - DateTime.Now).Milliseconds);
        }

        if (consumedefaultBucket) defaultBucket.Consume();
        if (buckets.ContainsKey(EndpointURL)) buckets[EndpointURL].Consume();

        PipedRequests--;
    }
}