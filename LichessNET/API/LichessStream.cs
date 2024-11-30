using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Vertical.SpectreLogger;

namespace LichessNET.API;

public class LichessStream
{
    // Define a delegate for the event
    public delegate void GameUpdateEventHandler(object sender, JObject gameUpdate);

    private static readonly HttpClient _httpClient = new HttpClient();

    private static int LichessStreamCounter = 0;

    private readonly ILogger _logger;
    private string requestUri = "https://lichess.org/api/bot/game/stream/gameId";


    public LichessStream(string requestURL)
    {
        requestUri = requestURL;


        var loggerFactory = LoggerFactory.Create(builder => builder
            .AddSpectreConsole());

        _logger = loggerFactory.CreateLogger("LichessStream_" + LichessStreamCounter);
    }

    // Declare the event using the delegate
    public event GameUpdateEventHandler GameUpdateReceived;

    public async Task StreamGameAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        LichessStreamCounter++;

        if (LichessStreamCounter > 5)
        {
            _logger.LogWarning("There are already " + LichessStreamCounter +
                               " active streams. The maximum number of streams per IP on Lichess is 8.");
        }

        if (LichessStreamCounter >= 8)
        {
            while (LichessStreamCounter > 7)
            {
                _logger.LogError(
                    "The maximum of streams for lichess is reached. This stream won't be prepared until another stream is closed.");
            }
        }

        using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
        {
            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var json = JObject.Parse(line);
                        // Raise the event when a game update is received
                        GameUpdateReceived?.Invoke(this, json);
                    }
                }
            }
        }

        LichessStreamCounter--;
    }
}