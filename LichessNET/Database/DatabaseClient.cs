using LichessNET.Extensions;
using Microsoft.Extensions.Logging;
using Vertical.SpectreLogger;
using ZstdNet;

namespace LichessNET.Database;

/// <summary>
/// A client handling accesses to the Lichess database and downloading big files.
/// You can get access to the monthly databases.
/// </summary>
public class DatabaseClient
{
    private readonly ILogger _logger;

    public DatabaseClient()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder
            .AddSpectreConsole());
        _logger = loggerFactory.CreateLogger<DatabaseClient>();
    }

    public async Task DownloadMonthlyDatabase(int year, int month, string filename, bool forceDownload = false)
    {
        if (File.Exists(filename + ".pgn") & !forceDownload)
        {
            _logger.LogInformation("File already exists, it probably already contains the requested data.");
            return;
        }

        if (month < 1 || month > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12");
        }

        if (year < 2013)
        {
            throw new ArgumentOutOfRangeException(nameof(year), "Year must be 2013 or later");
        }

        string url =
            $"https://database.lichess.org/standard/lichess_db_standard_rated_{year}-{month.ToString().PadLeft(2, '0')}.pgn.zst";

        _logger.LogInformation($"Requesting database for {year}-{month.ToString().PadLeft(2, '0')}");

        using (var client = new HttpClientDownloadWithProgress(url, filename + ".zst"))
        {
            client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
            {
                _logger.LogInformation(
                    $"Downloading database for {year}-{month.ToString().PadLeft(2, '0')}: {progressPercentage}% ({totalBytesDownloaded.ToSIPrefix()}B/{totalFileSize?.ToSIPrefix()}B)");
            };

            await client.StartDownload();
        }

        using (var inputStream = new FileStream(filename + ".zst", FileMode.Open, FileAccess.Read))
        using (var outputStream = new FileStream(filename + ".pgn", FileMode.Create, FileAccess.Write))
        using (var decompressionStream = new DecompressionStream(inputStream))
        {
            await decompressionStream.CopyToAsync(outputStream);
        }

        //Deleting the compressed file to save space
        File.Delete(filename + ".zst");

        _logger.LogInformation($"File saved to {filename}.pgn");
    }
}