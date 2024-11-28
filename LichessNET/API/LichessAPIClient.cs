using System.Net;
using System.Net.Http.Headers;
using System.Web;
using Microsoft.Extensions.Logging;
using TokenBucket;
using Vertical.SpectreLogger;

namespace LichessNET.API;

/// <summary>
///     This class represents a client for the lichess API.
///     It handles all ratelimits and requests.
/// </summary>
public partial class LichessAPIClient
{
    private readonly ILogger logger;

    /// <summary>
    ///     Bucket handler for ratelimits
    /// </summary>
    private readonly APIRatelimitController ratelimitController = new();

    /// <summary>
    ///     The token to access the Lichess API
    /// </summary>
    internal string Token = "";

    /// <summary>
    ///     Creates a lichess API client, according to settings
    /// </summary>
    /// <param name="Token">The token for accessing the lichess API</param>
    public LichessAPIClient(string Token = "")
    {
        var loggerFactory = LoggerFactory.Create(builder => builder
            .AddFilter(level => level >= LogLevel.Trace)
            .AddSpectreConsole());

        logger = loggerFactory.CreateLogger("LichessAPIClient");


        this.Token = Token;
        if (Token != "")
            logger.LogInformation("Connecting to Lichess API with token");
        else
            logger.LogInformation("Connecting to Lichess API without token");

        if (!Token.Contains("_"))
            logger.LogWarning("The token provided may not be a valid lichess API token. Please check the token.");

        logger.LogInformation("Connection to Lichess API established.");

        ratelimitController.RegisterBucket("api/account", TokenBuckets.Construct().WithCapacity(5)
            .WithFixedIntervalRefillStrategy(3, TimeSpan.FromSeconds(15)).Build());
    }


    /// <summary>
    ///     Gets the UriBuilder objects for the lichess client.
    ///     If something changes in the future, it will be easy to change it.
    /// </summary>
    /// <param name="endpoint"></param>
    /// <returns></returns>
    private UriBuilder GetUriBuilder(string endpoint, params Tuple<string, string>[] QueryParameters)
    {
        var builder = new UriBuilder(Constants.BASE_URL + endpoint);
        builder.Port = -1;

        var query = HttpUtility.ParseQueryString(builder.Query);

        foreach (var param in QueryParameters) query[param.Item1] = param.Item2;

        builder.Query = query.ToString();

        return builder;
    }

    private HttpRequestMessage GetRequestScaffold(string endpoint, params Tuple<string, string>[] QueryParameters)
    {
        var request = new HttpRequestMessage();
        request.RequestUri = GetUriBuilder(endpoint, QueryParameters).Uri;
        return request;
    }

    private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, HttpMethod method = null)
    {
        if (method == null) method = HttpMethod.Get;
        ratelimitController.Consume(request.RequestUri.AbsolutePath, true);
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        logger.LogInformation("Sending request to " + request.RequestUri);
        var response = client.SendAsync(request).Result;
        if (response.IsSuccessStatusCode)
        {
            logger.LogInformation("Request to " + request.RequestUri + " successful.");
            logger.LogInformation("Response: \n" + response.Content.ReadAsStringAsync().Result);
            return response;
        }

        if (response.StatusCode == HttpStatusCode.TooManyRequests)
        {
            logger.LogError("Ratelimited by Lichess API. Waiting for 60 seconds.");
            ratelimitController.ReportBlock();
            return null;
        }

        logger.LogError("Error while fetching data from Lichess API. Status code: " + response.StatusCode);
        logger.LogInformation("Response: \n" + response.Content.ReadAsStringAsync().Result);
        return null;
    }
}