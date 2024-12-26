using System.Text.Json;
using LichessNET.API;
using LichessNET.Entities.Enumerations;
using LichessNET.Entities.Game;

var client = new LichessApiClient(File.ReadAllText("token.txt"));

var allLeaderboards = await client.GetAllLeaderboardsAsync();

foreach (var leaderboard in allLeaderboards)
{
    Console.WriteLine($"Leaderboard: {leaderboard.Key}");
    foreach (var entry in leaderboard.Value)
    {
        Console.WriteLine($"Rank: Username: {entry.Username}");
    }
}