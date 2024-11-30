using System.Text.Json;
using LichessNET.API;
using LichessNET.Entities.Enumerations;

var client = new LichessApiClient(File.ReadAllText("token.txt"));

var status = await client.GetLeaderboardAsync(10, Gamemode.Bullet);
Console.WriteLine(JsonSerializer.Serialize(status, new JsonSerializerOptions
{
    WriteIndented = true
}));
Thread.Sleep(1000);